﻿using BusinessObject.IdentityModel;
using BusinessObject.Model;
using Microsoft.AspNetCore.Mvc;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Request.Accounts;
using Service.RequestAndResponse.Response.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IAccountService
    {
        Task<BaseResponse<RegisterCustomerResponse>> RegisterCustomer(RegisterCustomerRequest request);
        Task<BaseResponse<LoginResponse>> Login(LoginRequest request);
        Task<BaseResponse<string>> Confirmation(string email, int code);
        Task<BaseResponse<ApiResponse>> RenewToken(TokenModel model);
        Task<BaseResponse<AccountResponse>> CreateAccount(AccountRequest createAccountDto);
        Task<BaseResponse<string>> UpdateAccountStatus(string userEmail, [FromBody] UpdateAccountStatusDto updateAccountStatusDto);
        Task<BaseResponse<string>> ResetPasswordToken(ResetTokenModel resetTokenModel);
        Task<BaseResponse<AccountResetPasswordResponse>> ResetPassword([FromBody] ResetToken resetToken);
        Task<BaseResponse<AccountChangePasswordResponse>> ChangePassword([FromBody] ChangePasswordModel changePassword);
        Task<Account> GetByStringId(string id);
        Task<List<NewUserDto>> GetAllAccountsAsync();
        Task<BaseResponse<GetTotalAccount>> GetTotalAccount();
    }
}
