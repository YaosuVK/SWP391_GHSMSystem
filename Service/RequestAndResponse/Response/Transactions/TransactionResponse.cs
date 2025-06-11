using BusinessObject.Model;
using Service.RequestAndResponse.Response.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.RequestAndResponse.Response.Transactions
{
    public class TransactionResponse
    {
        public string ResponseId { get; set; }

        public Appointment? Appointment { get; set; } = null!;

        public GetAccount? Account { get; set; } = null!;

        public string TmnCode { get; set; }

        public string TxnRef { get; set; }

        public long Amount { get; set; }

        public string OrderInfo { get; set; }

        public string ResponseCode { get; set; }

        public string Message { get; set; }

        public string BankTranNo { get; set; }

        public DateTime PayDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public string BankCode { get; set; }

        public string TransactionNo { get; set; }

        public string TransactionType { get; set; }

        public string TransactionStatus { get; set; }

        public string SecureHash { get; set; }
 
        public TransactionKind TransactionKind { get; set; }
       
        public StatusOfTransaction StatusTransaction { get; set; }
    }
}

