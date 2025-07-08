using BusinessObject.Model;
using DataAccessObject.BaseDAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObject
{
    public class TransactionDAO : BaseDAO<Transaction>
    {
        private readonly GHSMContext _context;

        public TransactionDAO(GHSMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountId(string accountId)
        {
            return await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Appointment)
                .Where(t => t.Account != null && t.Account.Id == accountId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            return await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Appointment)
                .ToListAsync();
        }

        public async Task<Transaction?> GetTransactionById(string transactionID)
        {
            return await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Appointment)
                .FirstOrDefaultAsync(t => t.ResponseId == transactionID);
        }

        public async Task<IEnumerable<Transaction>> GetListTransactionsByAppointmentId(int appointmentID)
        {
            return await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Appointment)
                .Where(t => t.Appointment.AppointmentID == appointmentID)
                .ToListAsync();
        }

        public async Task<Transaction?> GetTransactionByAppointmentId(int appointmentID)
        {
            return await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Appointment)
                .FirstOrDefaultAsync(t => t.Appointment.AppointmentID == appointmentID);
        }

        public async Task<Transaction?> ChangeTransactionStatusForAppointment(int? appointmentId, StatusOfTransaction newStatus)
        {
            var transaction = await _context.Transactions
                .Where(t => t.Appointment != null && t.Appointment.AppointmentID == appointmentId)
                .OrderByDescending(t => t.PayDate) // chọn transaction mới nhất
                .FirstOrDefaultAsync();

            if (transaction is null || transaction.StatusTransaction == newStatus)
                return transaction;

            transaction.StatusTransaction = newStatus;
            await _context.SaveChangesAsync();

            return transaction;
        }
    }
}
