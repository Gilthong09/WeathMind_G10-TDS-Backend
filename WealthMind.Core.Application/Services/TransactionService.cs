using AutoMapper;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.Services.MainServices;
using WealthMind.Core.Application.ViewModels.TransactionV;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Services
{
    public class TransactionService : GenericService<SaveTransactionViewModel, TransactionViewModel, Transaction>, ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper) : base(transactionRepository, mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        ////Primera Version del Metodo
        //public async Task<decimal> GetTotalIncomeAsync(string userId, int year, int month)
        //{
        //    var transactions = await _transactionRepository.GetAllAsync();

        //    decimal totalIncome = transactions
        //        .Where(t => t.UserId == userId && t.TrxDate.Year == year && t.TrxDate.Month == month && (t.TransactionType == "Depósito" || t.TransactionType == "Ganancia"))
        //        .Sum(t => t.Amount);

        //    return totalIncome;
        //}
        
    }
}
