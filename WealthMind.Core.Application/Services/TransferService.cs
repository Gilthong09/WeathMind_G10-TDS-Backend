using AutoMapper;
using WealthMind.Core.Application.DTOs.Account;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.Services.MainServices;
using WealthMind.Core.Application.ViewModels.TransactionV;
using WealthMind.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Transactions;
using System.Security.Cryptography;

namespace WealthMind.Core.Application.Services
{
    public class TransferService : ITransferService
    {
        private readonly ITransactionService _transactionService;
        private readonly ISavingService _savingService;
        private readonly ILoanService _loanService;
        private readonly ICreditCardService _creditCardService;
        private readonly ICashService _cashService;
        private readonly IInvestmentService _investmentService;
        private readonly AuthenticationResponse user;
        private readonly IMapper _mapper;


        public TransferService(ITransactionService transactionService, IMapper mapper) : base(transactionService, mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;

        }
        public async Task<SaveTransactionViewModel> Transfer(SaveTransactionViewModel vm, Enums.TransactionType transactionType)
        {
            vm.TransactionTypeId = (int)transactionType;
            vm.TrxDate = DateTime.Now;

           
                var addMoney = await AddMoneyToAccount(vm.EntitySourceId, vm.EntityDestinationId, vm.Amount);
                if (!addMoney)
                {
                    return null;
                }

            

            if (vm.Description == null || vm.Description.Length == 0)
            {
                vm.Description = "Transfer";
            }

            return await _transactionService.Add(vm);

        }
        public async Task<bool> AddMoneyToAccount(string accountNumberOrigin, string accountNumberDestination, decimal amount)
        {
            /*
            var client = await _clientService.GetByUserIdViewModel(user.Id);

            var destinyAccount = await _savingService.GetByAccountNumberLoggedUser(accountNumberDestination, client.Id);
            var originAccount = await _savingService.GetByAccountNumberLoggedUser(accountNumberOrigin, client.Id);

            if (destinyAccount != null && originAccount != null)
            {
                if (amount > destinyAccount.Balance)
                {
                    throw new Exception("You can't do this transaction, the amount is higher than the balance of the account.");
                }

                double destinyBalance = destinyAccount.Balance + amount;
                double originBalance = originAccount.Balance - amount;

                await _savingService.UpdateSavingsAccount(destinyBalance, client.Id, destinyAccount.Id);
                await _savingService.UpdateSavingsAccount(originBalance, client.Id, originAccount.Id);
                return true;
            }
            else
            {
                throw new Exception("Operation failed.");
            }
            */
            Random rnd = new Random();
            var num =  rnd.Next(10);
            var booli = false;
            if( num < 10) {
                booli = true;
            }
            
            return booli;
        }

        /*
        public async Task<bool> AddMoneyToCreditCard(string accountNumberOrigin, string accountNumberDestination, decimal amount)
        {

        }

        public async Task<bool> AddCash(string accountNumberOrigin, string accountNumberDestination, decimal amount)
        {

        }
        */

    }
}
