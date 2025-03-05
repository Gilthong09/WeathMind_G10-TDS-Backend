using AutoMapper;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.Services.MainServices;
using WealthMind.Core.Application.ViewModels.CashV;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Services
{
    public class CashService : GenericService<SaveCashViewModel, CashViewModel, Cash>, ICashService
    {
        private readonly ICashRepository _cashRepository;
        private readonly IMapper _mapper;

        public CashService(ICashRepository cashRepository, IMapper mapper) : base(cashRepository, mapper)
        {
            _cashRepository = cashRepository;
            _mapper = mapper;
        }
    }
}
