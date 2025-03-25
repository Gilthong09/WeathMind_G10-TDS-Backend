using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthMind.Core.Application.ViewModels.ReportV;
using WealthMind.Core.Domain.Entities;

namespace WealthMind.Core.Application.Interfaces.Services
{
    public interface IReportService: IGenericService<SaveReportViewModel, ReportViewModel, Report>
    {

    }
}
