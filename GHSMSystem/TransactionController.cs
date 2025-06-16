using Microsoft.AspNetCore.Mvc;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Response.Transactions;

namespace GHSMSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionsService;
        public TransactionController(ITransactionService transactionsService)
        {
            _transactionsService = transactionsService;
        }

        [HttpGet]
        [Route("GetAllTransactions")]
        public async Task<ActionResult<BaseResponse<IEnumerable<TransactionResponse>>>> GetAllTransactions()
        {
            var transactions = await _transactionsService.GetAllTransactions();
            return Ok(transactions);
        }

        [HttpGet]
        [Route("GetTransactionByID/{transactionID}")]
        public async Task<ActionResult<BaseResponse<TransactionResponse?>>> GetTransactionById(string transactionID)
        {
            var transaction = await _transactionsService.GetTransactionById(transactionID);
            return Ok(transaction);
        }

        [HttpGet]
        [Route("GetTransactionByAppointmentId/{appointmentID}")]
        public async Task<ActionResult<BaseResponse<TransactionResponse?>>> GetTransactionByAppointmentId(int appointmentID)
        {
            var transaction = await _transactionsService.GetTransactionByAppointmentId(appointmentID);
            return Ok(transaction);
        }

        [HttpGet]
        [Route("GetTransactionByAccountID/{accountId}")]
        public async Task<ActionResult<BaseResponse<IEnumerable<TransactionResponse>>>> GetTransactionsByAccountId(string accountId)
        {
            var transactions = await _transactionsService.GetTransactionsByAccountId(accountId);
            return Ok(transactions);
        }
    }
}
