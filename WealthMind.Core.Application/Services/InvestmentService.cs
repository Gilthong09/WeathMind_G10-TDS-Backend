using AutoMapper;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.Services.MainServices;
using WealthMind.Core.Application.ViewModels.InvestmentV;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Services
{
    public class InvestmentService : GenericService<SaveInvestmentViewModel, InvestmentViewModel, Investment>, IInvestmentService
    {
        private readonly IInvestmentRepository _cashRepository;
        private readonly IMapper _mapper;

        public InvestmentService(IInvestmentRepository cashRepository, IMapper mapper) : base(cashRepository, mapper)
        {
            _cashRepository = cashRepository;
            _mapper = mapper;
        }
    }
}
