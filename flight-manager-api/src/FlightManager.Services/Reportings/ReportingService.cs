using System.Collections.Generic;
using FlightManager.Libraries;
using FlightManager.Repositories;
using FlightManager.Repositories.Models;
using FlightManager.Services.Models;

namespace FlightManager.Services
{
    public class ReportingService : IReportingService
    {
        private readonly IFlightRepository _flightRepository;

        public ReportingService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }
        
        public Result<List<ReportDTO>> GetReports()
        {
            var res = _flightRepository.SelectFlights();
            switch (res)
            {
                case Success<List<Flight>> success:
                    var reports = new List<ReportDTO>();
                    success.Value.ForEach(x => reports.Add(new ReportDTO(x)));
                    return Result<List<ReportDTO>>.Ok(reports);
                case Failure<List<Flight>> failure:
                    return Result<List<ReportDTO>>.Fail(failure.Errors);
                default:
                    return Result<List<ReportDTO>>.Fail(new List<string>() {"An error occured"});
            }
        }
    }
}