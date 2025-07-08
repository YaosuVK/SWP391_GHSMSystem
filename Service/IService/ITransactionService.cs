using BusinessObject.Model;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Response.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ITransactionService
    {
        Task<BaseResponse<IEnumerable<TransactionResponse>>> GetAllTransactions();
        Task<BaseResponse<IEnumerable<TransactionResponse>>> GetListTransactionsByAppointmentId(int appointmentID);
        Task<BaseResponse<IEnumerable<TransactionResponse>>> GetTransactionsByAccountId(string accountId);
        Task<BaseResponse<TransactionResponse?>> GetTransactionById(string transactionID);
        Task<BaseResponse<TransactionResponse?>> GetTransactionByAppointmentId(int appointmentID);
    }
}
