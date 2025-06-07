using AutoMapper;
using BusinessObject.IdentityModel;
using BusinessObject.Model;
using CloudinaryDotNet.Core;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Newtonsoft.Json.Linq;
using Repository.IBaseRepository;
using Repository.IRepositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Request.Accounts;
using Service.RequestAndResponse.Response.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IConsultantRepository _consultantRepository;
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly SignInManager<Account> _signinManager;

        public AccountService(IMapper mapper,
            IAccountRepository accountRepository,
            
            IConsultantRepository consultantRepository,
           
            UserManager<Account> userManager,
            RoleManager<IdentityRole> roleManager,
            ITokenRepository tokenRepository,
            SignInManager<Account> signinManager)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            
            _consultantRepository = consultantRepository;
            
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenRepository = tokenRepository;
            _signinManager = signinManager;
        }

        public async Task<BaseResponse<LoginResponse>> Login(LoginRequest request)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == request.Username.ToLower());

            if (user == null) 
            return new BaseResponse<LoginResponse>("Invalid username!", StatusCodeEnum.Unauthorized_401, null);

            var userEmail = await GetUser(user.Email);
            bool isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(userEmail);

            if (!isEmailConfirmed) 
            return new BaseResponse<LoginResponse>("You need to confirm email before login", StatusCodeEnum.BadRequest_400, null);

            if (user.Status == false)
            {
                return new BaseResponse<LoginResponse>("Cannot login with this account anymore!", StatusCodeEnum.Unauthorized_401, null);
            }


            var result = await _signinManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded) 
            return new BaseResponse<LoginResponse>("Username not found and/or password incorrect", StatusCodeEnum.Unauthorized_401, null);

            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null || !roles.Any())
            {
                return new BaseResponse<LoginResponse>("Sorry, Your account has Proplem to Identify who you are", StatusCodeEnum.BadRequest_400, null);
            }

            var token = await _tokenRepository.createToken(user);
            var loginResponse = new LoginResponse
            {
                UserID = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles.ToList(),
                Phone = user.Phone,
                isActive = user.Status,
                Name = user.Name,
                Address = user.Address,
                Token = token.AccessToken,
                RefreshToken = token.RefreshToken
            };

            return new BaseResponse<LoginResponse>("Register successfully", StatusCodeEnum.OK_200, loginResponse);
        }

        public async Task<BaseResponse<RegisterCustomerResponse>> RegisterCustomer(RegisterCustomerRequest request)
        {
            try
            {
                var dateOnly = DateOnly.FromDateTime(request.DateOfBirth);

                var accountApp = new Account
                {
                    UserName = request.Username,
                    Name = request.Name,
                    Email = request.Email,
                    Address = request.Address,
                    Phone = request.Phone,
                    Status = true,
                    DateOfBirth = dateOnly
                };
                var existUser = await _userManager.FindByEmailAsync(request.Email);
                if (existUser != null)
                {
                    return new BaseResponse<RegisterCustomerResponse>("Email already exists!", StatusCodeEnum.Conflict_409, null);
                }
                else
                {
                    var createdUser = await _userManager.CreateAsync(accountApp, request.Password);
                    if (createdUser.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(accountApp, "CUSTOMER");
                        var token = await _tokenRepository.createToken(accountApp);
                        if (roleResult.Succeeded)
                        {
                            var _user = await GetUser(request.Email);
                            var emailCode = await _userManager.GenerateEmailConfirmationTokenAsync(_user!);

                            if (string.IsNullOrEmpty(emailCode))
                            {
                                await _userManager.DeleteAsync(accountApp); // Xóa tài khoản để tránh bị kẹt
                                return new BaseResponse<RegisterCustomerResponse>("Failed to generate confirmation token. Please try again.", StatusCodeEnum.InternalServerError_500, null);
                            }

                            string sendEmail = SendEmail(_user!.Email!, emailCode);

                            if (string.IsNullOrEmpty(sendEmail)) // Nếu gửi thất bại
                            {
                                await _userManager.DeleteAsync(accountApp); // Xóa tài khoản để tránh bị kẹt
                                return new BaseResponse<RegisterCustomerResponse>("Failed to send email. Please try again.", StatusCodeEnum.InternalServerError_500, null);
                            }

                            var userRoles = await _userManager.GetRolesAsync(accountApp);
                            var customerResponse = new RegisterCustomerResponse
                            {
                                UserName = accountApp.UserName,
                                Email = accountApp.Email,
                                Name = accountApp.Name,
                                Address = accountApp.Address,
                                Phone = accountApp.Phone,
                                DateOfBirth = accountApp.DateOfBirth,
                                Roles = userRoles.ToList(),
                                Token = token.AccessToken,
                                RefreshToken = token.RefreshToken
                            };
                            return new BaseResponse<RegisterCustomerResponse>("Register successfully", StatusCodeEnum.OK_200, customerResponse);
                        }
                        else
                        {
                            return new BaseResponse<RegisterCustomerResponse>($"{roleResult.Errors}", StatusCodeEnum.InternalServerError_500, null);
                        }
                    }
                    else
                    {
                        return new BaseResponse<RegisterCustomerResponse>($"{createdUser.Errors}", StatusCodeEnum.InternalServerError_500, null);
                    }
                }
            }
            catch (Exception e)
            {
                var errorMessage = e.Message;

                // Nếu có lỗi bên trong, gộp lại
                if (e.InnerException != null)
                {
                    errorMessage += " | InnerException: " + e.InnerException.Message;

                    // Nếu còn lồng nữa (2 cấp), bạn có thể lấy tiếp:
                    if (e.InnerException.InnerException != null)
                    {
                        errorMessage += " | InnerInnerException: " + e.InnerException.InnerException.Message;
                    }
                }
                return new BaseResponse<RegisterCustomerResponse>($"{errorMessage}", StatusCodeEnum.InternalServerError_500, null);
            }
        }

        private async Task<Account> GetUser(string email)
        => await _userManager.FindByEmailAsync(email);

        private string SendEmail(string email, string emailCode)
        {
            StringBuilder emailMessage = new StringBuilder();
            emailMessage.Append("<html>");
            emailMessage.Append("<body>");
            emailMessage.Append($"<p>Dear {email},</p>");
            emailMessage.Append("<p>Thank you for your registering with us. To verifiy your email address, please use the following verification code: </p>");
            emailMessage.Append($"<h2>Verification Code: {emailCode}</h2>");
            emailMessage.Append("<p>Please enter this code on our website to complete your registration.</p>");
            emailMessage.Append("<p>If you did not request this, please ignore this email</p>");
            emailMessage.Append("<br>");
            emailMessage.Append("<p>Best regards,</p>");
            emailMessage.Append("<p><strong>GHSMSystem</strong></o>");
            emailMessage.Append("</body>");
            emailMessage.Append("</html>");

            string message = emailMessage.ToString();
            var _email = new MimeMessage();
            _email.To.Add(MailboxAddress.Parse(email));
            _email.From.Add(MailboxAddress.Parse("khanhvmse171632@fpt.edu.vn"));
            _email.Subject = "Email Confirmation";
            _email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("khanhvmse171632@fpt.edu.vn", "qpvj xjhk eihq sptw"); //user email and password qpvj xjhk eihq sptw
            smtp.Send(_email);
            smtp.Disconnect(true);
            return "Thank you for your registration, kindly check your email for confirmation code";
        }

        public async Task<BaseResponse<string>> Confirmation(string email, int code)
        {
            if (string.IsNullOrEmpty(email) || code <= 0)
            {
                return new BaseResponse<string>("Invalid Code Provided", StatusCodeEnum.BadRequest_400, null);
            }
            var user = await GetUser(email);
            if (user == null)
            {
                return new BaseResponse<string>("Invalid Indentity Provided", StatusCodeEnum.BadRequest_400, null);
            }
            var result = await _userManager.ConfirmEmailAsync(user, code.ToString());
            if (!result.Succeeded)
            {
                return new BaseResponse<string>("Invalid Code Provided", StatusCodeEnum.BadRequest_400, null);
            }
            else
            {
                return new BaseResponse<string>("Email confirm successfully, you can proceed to login", StatusCodeEnum.OK_200, null);
            }
        }

        public async Task<BaseResponse<ApiResponse>> RenewToken(TokenModel model)
        {
            var result = await _tokenRepository.renewToken(model);
            return new BaseResponse<ApiResponse>("Renew Token Successfully", StatusCodeEnum.OK_200, result);
        }

        public async Task<BaseResponse<AccountResponse>> CreateAccount( AccountRequest createAccountDto)
        {
            try
            {
                /*if (!ModelState.IsValid)
                    return BadRequest(ModelState);*/
                var dateOnly = DateOnly.FromDateTime(createAccountDto.DateOfBirth);

                if (string.IsNullOrEmpty(createAccountDto.Role))
                return new BaseResponse<AccountResponse>("Role is required.", StatusCodeEnum.BadRequest_400, null);

                var existUser = await _userManager.FindByEmailAsync(createAccountDto.Email);
                if (existUser != null)
                {
                    return new BaseResponse<AccountResponse>("This email has already registered, please try another email!!!", StatusCodeEnum.BadRequest_400, null);
                }

                var accountApp = new Account
                {
                    UserName = createAccountDto.Username,
                    Name = createAccountDto.Name,
                    Email = createAccountDto.Email,
                    Address = createAccountDto.Address,
                    Phone = createAccountDto.Phone,
                    Status = true,
                    DateOfBirth = dateOnly,
                    EmailConfirmed = true
                };

                var role = createAccountDto.Role.ToUpper();

                if (role == "ADMIN")
                {
                    return new BaseResponse<AccountResponse>("Cannot create account with the role 'Admin'.", StatusCodeEnum.BadRequest_400, null);
                }

                var createdUser = await _userManager.CreateAsync(accountApp, createAccountDto.Password);

                if (createdUser.Succeeded)
                {

                    var roleExists = await _roleManager.RoleExistsAsync(role);
                    if (roleExists)
                    {
                        if (createAccountDto.Role.ToUpper() == "ADMIN")
                        {
                            return new BaseResponse<AccountResponse>("Cannot create account with the role 'Admin'.", StatusCodeEnum.BadRequest_400, null);
                        }
                        var roleResult = await _userManager.AddToRoleAsync(accountApp, createAccountDto.Role);

                        if (roleResult.Succeeded)
                        {
                            var userRoles = await _userManager.GetRolesAsync(accountApp);
                            var token = await _tokenRepository.createToken(accountApp);
                            var accountResponse = new AccountResponse
                            {
                                UserName = accountApp.UserName,
                                Email = accountApp.Email,
                                Name = accountApp.Name,
                                Address = accountApp.Address,
                                Phone = accountApp.Phone,
                                DateOfBirth = accountApp.DateOfBirth,
                                Roles = userRoles.ToList(),
                                Token = token.AccessToken,
                                isActive = accountApp.Status,
                                RefreshToken = token.RefreshToken
                            };
                            return new BaseResponse<AccountResponse>("Register successfully", StatusCodeEnum.OK_200, accountResponse);
                        }
                        else
                        {
                            return new BaseResponse<AccountResponse>($"{roleResult.Errors}", StatusCodeEnum.InternalServerError_500, null);
                        }
                    }
                    else
                    {
                        await _userManager.DeleteAsync(accountApp);
                        return new BaseResponse<AccountResponse>($"Role '{createAccountDto.Role}' does not exist.", StatusCodeEnum.InternalServerError_500, null);
                    }
                }
                else
                {
                    return new BaseResponse<AccountResponse>($"{createdUser.Errors}", StatusCodeEnum.InternalServerError_500, null);
                }
            }
            catch (Exception e)
            {
                var errorMessage = e.Message;

                // Nếu có lỗi bên trong, gộp lại
                if (e.InnerException != null)
                {
                    errorMessage += " | InnerException: " + e.InnerException.Message;

                    // Nếu còn lồng nữa (2 cấp), bạn có thể lấy tiếp:
                    if (e.InnerException.InnerException != null)
                    {
                        errorMessage += " | InnerInnerException: " + e.InnerException.InnerException.Message;
                    }
                }
                return new BaseResponse<AccountResponse>($"{errorMessage}", StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<AccountResponse>> UpdateAccount(string userId, UpdateAccountDto updateAccountDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return new BaseResponse<AccountResponse>($"User with ID '{userId}' not found.", StatusCodeEnum.NotFound_404, null);
                }
                if (!string.IsNullOrEmpty(updateAccountDto.Username))
                {
                    user.UserName = updateAccountDto.Username;
                }

                // Update role if provided
                if (!string.IsNullOrEmpty(updateAccountDto.Role))
                {
                    // Check if the new role exists
                    var newRole = updateAccountDto.Role.ToUpper();
                    var roleExists = await _roleManager.RoleExistsAsync(newRole);
                    if (!roleExists)
                    {
                        return new BaseResponse<AccountResponse>($"Role '{newRole}' does not exist.", StatusCodeEnum.BadRequest_400, null);
                    }

                    // Remove current roles and assign new role
                    var currentRoles = await _userManager.GetRolesAsync(user);
                    var roleRemoveResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    if (!roleRemoveResult.Succeeded)
                    {
                        return new BaseResponse<AccountResponse>($"{roleRemoveResult.Errors}", StatusCodeEnum.InternalServerError_500, null);
                    }

                    var roleAddResult = await _userManager.AddToRoleAsync(user, newRole);
                    if (!roleAddResult.Succeeded)
                    {
                        return new BaseResponse<AccountResponse>($"{roleAddResult.Errors}", StatusCodeEnum.InternalServerError_500, null);
                    }
                }

                user.Name = updateAccountDto.Name ?? user.Name;
                user.Address = updateAccountDto.Address ?? user.Address;
                user.Phone = updateAccountDto.Phone ?? user.Phone;

                var updateResult = await _userManager.UpdateAsync(user);

                if (updateResult.Succeeded)
                {
                   
                    var updatedUserRoles = await _userManager.GetRolesAsync(user);
                    var accountResponse = new AccountResponse
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Name = user.Name,
                        Address = user.Address,
                        Phone = user.Phone,
                        Roles = updatedUserRoles.ToList()
                    };
                    return new BaseResponse<AccountResponse>("Update successfully", StatusCodeEnum.OK_200, accountResponse);
                }
                else
                {
                    return new BaseResponse<AccountResponse>($"{updateResult.Errors}", StatusCodeEnum.InternalServerError_500, null);
                }
            }
            catch (Exception e)
            {
                return new BaseResponse<AccountResponse>($"{e.Message}", StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<string>> UpdateAccountStatus(string userEmail, [FromBody] UpdateAccountStatusDto updateAccountStatusDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    return new BaseResponse<string>($"User with Email '{userEmail}' not found.", StatusCodeEnum.NotFound_404, null);
                }
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("ADMIN"))
                {
                    return new BaseResponse<string>("Admin cannot banned by him/herself", StatusCodeEnum.BadRequest_400, null);
                }
                user.Status = updateAccountStatusDto.Status;
                var updateResult = await _userManager.UpdateAsync(user);
                if (updateResult.Succeeded)
                {
                    return new BaseResponse<string>("Update successfully", StatusCodeEnum.OK_200, null);
                }
                else
                {
                    return new BaseResponse<string>($"{updateResult.Errors}", StatusCodeEnum.InternalServerError_500, null);
                }

            }
            catch (Exception e)
            {
                var errorMessage = e.Message;

                // Nếu có lỗi bên trong, gộp lại
                if (e.InnerException != null)
                {
                    errorMessage += " | InnerException: " + e.InnerException.Message;

                    // Nếu còn lồng nữa (2 cấp), bạn có thể lấy tiếp:
                    if (e.InnerException.InnerException != null)
                    {
                        errorMessage += " | InnerInnerException: " + e.InnerException.InnerException.Message;
                    }
                }
                return new BaseResponse<string>($"{errorMessage}", StatusCodeEnum.InternalServerError_500, null);
            }
        }

        public async Task<BaseResponse<string>> ResetPasswordToken( ResetTokenModel resetTokenModel)
        {
            var user = await _userManager.FindByEmailAsync(resetTokenModel.Email);
            if (user == null)
            {
                return new BaseResponse<string>("Cannot find Email. Please check again!", StatusCodeEnum.NotFound_404, null);
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return new BaseResponse<string>("Update successfully", StatusCodeEnum.OK_200, token);
        }

        public async Task<BaseResponse<AccountResetPasswordResponse>> ResetPassword([FromBody] ResetToken resetToken)
        {
            var user = await _userManager.FindByEmailAsync(resetToken.Email);
            if (user == null)
            {
                return new BaseResponse<AccountResetPasswordResponse>("UserName is wrong, Please check again!", StatusCodeEnum.BadRequest_400, null);
            }

            user = await _userManager.FindByNameAsync(resetToken.Username);
            if (user == null)
            {
                return new BaseResponse<AccountResetPasswordResponse>("Cannot find Email, Please check again!", StatusCodeEnum.BadRequest_400, null);
            }

            if (string.Compare(resetToken.Password, resetToken.ConfirmPassword) != 0)
            {
                return new BaseResponse<AccountResetPasswordResponse>("Password and ConfirmPassword doesnot match! ", StatusCodeEnum.BadRequest_400, null);
            }
            if (string.IsNullOrEmpty(resetToken.Token))
            {
                return new BaseResponse<AccountResetPasswordResponse>("Invalid Token! ", StatusCodeEnum.BadRequest_400, null);
            }
            var result = await _userManager.ResetPasswordAsync(user, resetToken.Token, resetToken.Password);
            if (!result.Succeeded)
            {
                var errors = new List<string>();
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
                return new BaseResponse<AccountResetPasswordResponse>($"I{result.Errors}", StatusCodeEnum.InternalServerError_500, null);
            }
            
            var response = new AccountResetPasswordResponse
            {
                Email = resetToken.Email,
                Username = resetToken.Username,
                Password = resetToken.Password,
                ConfirmPassword = resetToken.ConfirmPassword,
                Token = resetToken.Token
            };
            return new BaseResponse<AccountResetPasswordResponse>($"I{result.Errors}", StatusCodeEnum.OK_200, response);

        }

        public async Task<BaseResponse<AccountChangePasswordResponse>> ChangePassword([FromBody] ChangePasswordModel changePassword)
        {
            var user = await _userManager.FindByNameAsync(changePassword.UserName);
            if (user == null)
            {
                return new BaseResponse<AccountChangePasswordResponse>("User Not Exist", StatusCodeEnum.BadRequest_400, null);
            }
            if (string.Compare(changePassword.NewPassword, changePassword.ConfirmNewPassword) != 0)
            {
                return new BaseResponse<AccountChangePasswordResponse>("Password and ConfirmPassword doesnot match! ", StatusCodeEnum.BadRequest_400, null);
            }
            var result = await _userManager.ChangePasswordAsync(user, changePassword.CurrentPassword, changePassword.NewPassword);
            if (!result.Succeeded)
            {
                var errors = new List<string>();
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
                return new BaseResponse<AccountChangePasswordResponse>($"{result.Errors}", StatusCodeEnum.InternalServerError_500, null);
            }
            
            var response = new AccountChangePasswordResponse
            {
                Username = changePassword.UserName,
                Password = changePassword.NewPassword,
                ConfirmPassword = changePassword.ConfirmNewPassword
            };
            return new BaseResponse<AccountChangePasswordResponse>($"I{result.Errors}", StatusCodeEnum.OK_200, response);
        }
    }
}
