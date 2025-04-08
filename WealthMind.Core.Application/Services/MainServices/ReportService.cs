using AutoMapper;
using WealthMind.Core.Application.Interfaces.Repositories;
using WealthMind.Core.Application.Interfaces.Services;
using WealthMind.Core.Application.ViewModels.ReportV;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Services.MainServices
{
    public class ReportService : GenericService<SaveReportViewModel, ReportViewModel, Report>, IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public ReportService(IReportRepository reportRepository, IMapper mapper) : base(reportRepository, mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }


    }
}
