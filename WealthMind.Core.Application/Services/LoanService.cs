using AutoMapper;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.Services.MainServices;
using WealthMind.Core.Application.ViewModels.LoanV;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Services
{
    public class LoanService : GenericService<SaveLoanViewModel, LoanViewModel, Loan>, ILoanService
    {
        private readonly ILoanRepository _cashRepository;
        private readonly IMapper _mapper;

        public LoanService(ILoanRepository cashRepository, IMapper mapper) : base(cashRepository, mapper)
        {
            _cashRepository = cashRepository;
            _mapper = mapper;
        }
    }
}
