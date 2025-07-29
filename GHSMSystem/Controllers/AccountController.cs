using BusinessObject.IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.Accounts;
using Service.RequestAndResponse.Response.Accounts;
using System.Reflection.Metadata.Ecma335;

namespace GHSMSystem.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("adminDashBoard/GetTotalAccount")]
        public async Task<BaseResponse<GetTotalAccount>> GetTotalAccount()
        {
            return await _accountService.GetTotalAccount();
        }

        [HttpGet]
        [Route("GetAllAccounts")]
        public async Task<List<NewUserDto>> GetAllAccountsAsync()
        {
            return await _accountService.GetAllAccountsAsync();
        }

        [HttpPost("login")]
        public async Task<BaseResponse<LoginResponse>> Login(LoginRequest request)
        {
            return await _accountService.Login(request);
        }

        [HttpPost("register-Customer")]
        public async Task<ActionResult<BaseResponse<RegisterCustomerResponse>>> RegisterCustomer(RegisterCustomerRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                return BadRequest(new { Message = "Dữ liệu không hợp lệ", Errors = errors });
            }
            return await _accountService.RegisterCustomer(request);
        }

        [HttpPost("confirmation/{email}/{code:int}")]
        public async Task<BaseResponse<string>> Confirmation(string email, int code)
        {
            return await _accountService.Confirmation(email, code);
        }

        [HttpPost("resetToken")]
        public async Task<BaseResponse<ApiResponse>> RenewToken(TokenModel model)
        {
            return await _accountService.RenewToken(model);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("create account")]
        public async Task<ActionResult<BaseResponse<AccountResponse>>> CreateAccount(AccountRequest createAccountDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                return BadRequest(new { Message = "Dữ liệu không hợp lệ", Errors = errors });
            }
            return await _accountService.CreateAccount(createAccountDto);
        }

        [HttpPut("Update-Account-Status")]
        public async Task<BaseResponse<string>> UpdateAccountStatus(string userEmail, [FromBody] UpdateAccountStatusDto updateAccountStatusDto)
        {
            return await _accountService.UpdateAccountStatus(userEmail, updateAccountStatusDto);
        }

        [HttpPost("Reset-Password-Token")]
        public async Task<BaseResponse<string>> ResetPasswordToken(ResetTokenModel resetTokenModel)
        {
            return await _accountService.ResetPasswordToken(resetTokenModel);
        }

        [HttpPost("Reset-Password")]
        public async Task<BaseResponse<AccountResetPasswordResponse>> ResetPassword([FromBody] ResetToken resetToken)
        {
            return await _accountService.ResetPassword(resetToken);
        }

        [HttpPost("Change-Password")]
        public async Task<BaseResponse<AccountChangePasswordResponse>> ChangePassword([FromBody] ChangePasswordModel changePassword)
        {
            return await _accountService.ChangePassword(changePassword);
        }
    }
}
