using AutoMapper;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.Services.MainServices;
using WealthMind.Core.Application.ViewModels.CreditCard;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Services
{
    public class CreditCardService : GenericService<SaveCreditCardViewModel, CreditCardViewModel, CreditCard>, ICreditCardService
    {
        private readonly ICreditCardRepository _cashRepository;
        private readonly IMapper _mapper;

        public CreditCardService(ICreditCardRepository cashRepository, IMapper mapper) : base(cashRepository, mapper)
        {
            _cashRepository = cashRepository;
            _mapper = mapper;
        }
    }
}
