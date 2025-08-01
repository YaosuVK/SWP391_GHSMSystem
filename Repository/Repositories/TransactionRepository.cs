﻿using BusinessObject.Model;
using DataAccessObject;
using Repository.BaseRepository;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        private readonly TransactionDAO _transactionDao;

        public TransactionRepository(TransactionDAO transactionDao) : base(transactionDao)
        {
            _transactionDao = transactionDao;
        }

        public async Task<Transaction?> ChangeTransactionStatusForAppointment(int? appointmentId, StatusOfTransaction newStatus)
        {
            return await _transactionDao.ChangeTransactionStatusForAppointment(appointmentId, newStatus);
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            return await _transactionDao.GetAllTransactions();
        }

        public async Task<IEnumerable<Transaction>> GetListTransactionsByAppointmentId(int appointmentID)
        {
            return await _transactionDao.GetListTransactionsByAppointmentId(appointmentID);
        }

        public async Task<Transaction?> GetTransactionByAppointmentId(int appointmentID)
        {
            return await _transactionDao.GetTransactionByAppointmentId(appointmentID);
        }

        public async Task<Transaction?> GetTransactionById(string transactionID)
        {
            return await _transactionDao.GetTransactionById(transactionID);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountId(string accountId)
        {
            return await _transactionDao.GetTransactionsByAccountId(accountId);
        }
    }
}
