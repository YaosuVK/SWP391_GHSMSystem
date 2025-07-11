﻿using AutoMapper;
using BusinessObject.Model;
using Repository.IRepositories;
using Service.IService;
using Service.RequestAndResponse.BaseResponse;
using Service.RequestAndResponse.Enums;
using Service.RequestAndResponse.Response.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(IMapper mapper, ITransactionRepository transactionRepository)
        {
            _mapper = mapper;
            _transactionRepository = transactionRepository;
        }

        public async Task<BaseResponse<IEnumerable<TransactionResponse>>> GetAllTransactions()
        {
            IEnumerable<Transaction> transaction = await _transactionRepository.GetAllTransactions();
            if (transaction == null || !transaction.Any())
            {
                return new BaseResponse<IEnumerable<TransactionResponse>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            var transactions = _mapper.Map<IEnumerable<TransactionResponse>>(transaction);
            if (transactions == null || !transactions.Any())
            {
                return new BaseResponse<IEnumerable<TransactionResponse>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<IEnumerable<TransactionResponse>>("Get all transactions as base success",
                StatusCodeEnum.OK_200, transactions);
        }

        public async Task<BaseResponse<IEnumerable<TransactionResponse>>> GetListTransactionsByAppointmentId(int appointmentID)
        {
            IEnumerable<Transaction> transaction = await _transactionRepository.GetListTransactionsByAppointmentId(appointmentID);
            if (transaction == null || !transaction.Any())
            {
                return new BaseResponse<IEnumerable<TransactionResponse>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            var transactions = _mapper.Map<IEnumerable<TransactionResponse>>(transaction);
            if (transactions == null || !transactions.Any())
            {
                return new BaseResponse<IEnumerable<TransactionResponse>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<IEnumerable<TransactionResponse>>("Get all transactions as base success",
                StatusCodeEnum.OK_200, transactions);
        }

        public async Task<BaseResponse<TransactionResponse?>> GetTransactionByAppointmentId(int appointmentID)
        {
            Transaction? transaction = await _transactionRepository.GetTransactionByAppointmentId(appointmentID);
            var result = _mapper.Map<TransactionResponse>(transaction);
            if (result == null)
            {
                return new BaseResponse<TransactionResponse?>("Something Went Wrong!", StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<TransactionResponse?>("Get Transaction as base success", StatusCodeEnum.OK_200, result);
        }

        public async Task<BaseResponse<TransactionResponse?>> GetTransactionById(string transactionID)
        {
            Transaction? transaction = await _transactionRepository.GetTransactionById(transactionID);
            var result = _mapper.Map<TransactionResponse>(transaction);
            if (result == null)
            {
                return new BaseResponse<TransactionResponse?>("Something Went Wrong!", StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<TransactionResponse?>("Get Transaction as base success", StatusCodeEnum.OK_200, result);
        }

        public async Task<BaseResponse<IEnumerable<TransactionResponse>>> GetTransactionsByAccountId(string accountId)
        {
            IEnumerable<Transaction> transaction = await _transactionRepository.GetTransactionsByAccountId(accountId);
            if (transaction == null || !transaction.Any())
            {
                return new BaseResponse<IEnumerable<TransactionResponse>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            var transactions = _mapper.Map<IEnumerable<TransactionResponse>>(transaction);
            if (transactions == null || !transactions.Any())
            {
                return new BaseResponse<IEnumerable<TransactionResponse>>("Something went wrong!",
                StatusCodeEnum.BadGateway_502, null);
            }
            return new BaseResponse<IEnumerable<TransactionResponse>>("Get all transactions as base success",
                StatusCodeEnum.OK_200, transactions);
        }
    }
}
