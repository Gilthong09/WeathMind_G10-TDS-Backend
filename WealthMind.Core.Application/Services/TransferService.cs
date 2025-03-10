using AutoMapper;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.Services.MainServices;
using WealthMind.Core.Application.ViewModels.TransactionV;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Services
{
    public class TransferService : GenericService<SaveTransactionViewModel, TransactionViewModel, Transaction>
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public TransferService(ITransactionRepository transactionRepository, IMapper mapper) : base(transactionRepository, mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }
        //public async Task<SaveTransactionViewModel> Transfer()
        //{

        //}
        //public async Task<bool> AddMoneyToAccount()
        //{

        //}
    }
}
