using AutoMapper;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.Services.MainServices;
using WealthMind.Core.Application.ViewModels.SavingV;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Services
{
    public class SavingService : GenericService<SaveSavingViewModel, SavingViewModel, Saving>, ISavingService
    {
        private readonly ISavingRepository _savingRepository;
        private readonly IMapper _mapper;

        public SavingService(ISavingRepository savingRepository, IMapper mapper) : base(savingRepository, mapper)
        {
            _savingRepository = savingRepository;
            _mapper = mapper;
        }
    }
}
