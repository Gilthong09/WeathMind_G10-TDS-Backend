using AutoMapper;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Services.MainServices;
using WealthMind.Core.Application.ViewModels.FinancialGoal;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Services
{
    public class FinancialGoalService : GenericService<SaveFinancialGoalViewModel, FinancialGoalViewModel, FinancialGoal>, IFinancialGoalService
    {
        private readonly IFinancialGoalRepository _financialGoalRepository;
        private readonly IMapper _mapper;

        public FinancialGoalService(IFinancialGoalRepository financialGoalRepository, IMapper mapper) : base(financialGoalRepository, mapper)
        {
            _financialGoalRepository = financialGoalRepository;
            _mapper = mapper;
        }
    }
}
