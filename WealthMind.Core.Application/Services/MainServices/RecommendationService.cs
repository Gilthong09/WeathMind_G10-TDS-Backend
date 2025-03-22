using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.ViewModels.RecommendationV;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Services.MainServices
{
    public class RecommendationService: GenericService<SaveRecommendationViewModel, RecommendationViewModel, Recommendation>, IRecommendationService
    {
        private readonly IRecommendationRepository _recommendationRepository;
        private readonly IMapper _mapper;

        public RecommendationService(IRecommendationRepository recommendationRepository, IMapper mapper) : base(recommendationRepository, mapper)
        {
            _recommendationRepository = recommendationRepository;
            _mapper = mapper;
        }
    }
}
