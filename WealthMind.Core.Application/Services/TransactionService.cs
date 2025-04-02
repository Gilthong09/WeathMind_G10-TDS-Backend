using AutoMapper;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.Services.MainServices;
using WealthMind.Core.Application.ViewModels.TransactionV;
using WealthMind.Core.Domain.Entities;
using WealthMind.Core.Domain.Statistics;

namespace WealthMind.Core.Application.Services
{
    public class TransactionService : GenericService<SaveTransactionViewModel, TransactionViewModel, Transaction>, ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Servicio para la gestión de transacciones.
        /// </summary>
        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper) : base(transactionRepository, mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<List<TransactionViewModel>> GetTransactionsByUserIdAsync(string userId)
        {
            var listTransactions = await _transactionRepository.GetTransactionsByUserIdAsync(userId);
            return _mapper.Map<List<TransactionViewModel>>(listTransactions);
        }

        public async Task<List<TransactionViewModel>> GetTransactionsByCategoryAsync(string categoryId, string userId)
        {
            var listTransactions = await _transactionRepository.GetTransactionsByCategoryAsync(categoryId, userId);
            return _mapper.Map<List<TransactionViewModel>>(listTransactions);
        }

        public async Task<List<TransactionViewModel>> GetTransactionsByDateRangeAsync(string userId, DateTime startDate, DateTime endDate)
        {
            var listTransactions = await _transactionRepository.GetTransactionsByDateRangeAsync(userId, startDate, endDate);
            return _mapper.Map<List<TransactionViewModel>>(listTransactions);
        }

        public async Task<decimal> GetTotalIncomeAsync(string userId, int year, int month)
        {
            return await _transactionRepository.GetTotalIncomeAsync(userId, year, month);
        }

        public async Task<decimal> GetTotalExpensesAsync(string userId, int year, int month)
        {
            return await _transactionRepository.GetTotalExpensesAsync(userId, year, month);
        }

        public async Task<List<TransactionViewModel>> GetTopExpensesByCategoryAsync(string userId, int year, int month, int topN)
        {
            var listTransactions = await _transactionRepository.GetTopExpensesByCategoryAsync(userId, year, month, topN);
            return _mapper.Map<List<TransactionViewModel>>(listTransactions);
        }

        public async Task<MonthlyStatistics> SpendingPercentageByCategoryAsync(string userId, int year, int month)
        {
            //try
            //{
            //    var ms = await _transactionRepository.SpendingPercentageByCategoryAsync(userId, year, month);
            //}catch(Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}
            


            return await _transactionRepository.SpendingPercentageByCategoryAsync(userId, year, month);
        }

        public async Task<AnnualStatistics> GetAnnualSpendingPercentageByCategoryAsync(string userId, int year)
        {
            return await _transactionRepository.GetAnnualSpendingPercentageByCategoryAsync(userId, year);
        }

    }
}
