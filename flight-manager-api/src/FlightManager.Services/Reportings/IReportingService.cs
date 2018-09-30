using System.Collections.Generic;
using FlightManager.Libraries;
using FlightManager.Services.Models;

namespace FlightManager.Services
{
    public interface IReportingService
    {
        Result<List<ReportDTO>> GetReports();
    }
}