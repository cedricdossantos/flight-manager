using System.Collections.Generic;
using FlightManager.Libraries;
using FlightManager.Repositories;
using FlightManager.Repositories.Models;
using FlightManager.Services.Models;

namespace FlightManager.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;

        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public Result AddFlight(FlightDTO flightDto)
        {
            var res = _flightRepository.CreateFlight(flightDto.ToDbModel());

            switch (res)
            {
                case Success success:
                    return Result.Ok(success.Message);
                case Conflict conflict:
                    return Result.Conflict(conflict.Error);
                case Failure failure:
                    return Result.Fail(failure.Errors);
                default:
                    return Result.Fail(new List<string>() {"An error occured"});
            }
        }

        public Result UpdateFlight(FlightDTO flightDto)
        {
            var res = _flightRepository.UpdateFlight(flightDto.ToDbModel());
            switch (res)
            {
                case Success success:
                    return Result.Ok(success.Message);
                case Failure failure:
                    return Result.Fail(failure.Errors);
                case NotFound notFound :
                    return Result.NotFound(notFound.Error);
                default:
                    return Result.Fail(new List<string>() {"An error occured"});
        }
        }

        public Result<FlightDTO> GetFlight(string code)
        {
            var res = _flightRepository.SelectFlight(code);
            switch (res)
            {
                case Success<Flight> success:
                    return Result<FlightDTO>.Ok(new FlightDTO(success.Value));
                case Failure<Flight> failure:
                    return Result<FlightDTO>.Fail(failure.Errors);
                case NotFound<Flight> notFound :
                    return Result<FlightDTO>.NotFound(notFound.Error);
                default:
                    return Result<FlightDTO>.Fail(new List<string>() {"An error occured"});
            }
        }

        public Result<List<FlightDTO>> GetFlights()
        {
            var res = _flightRepository.SelectFlights();
            switch (res)
            {
                case Success<List<Flight>> success:
                    var flights = new List<FlightDTO>();
                    success.Value.ForEach(x => flights.Add(new FlightDTO(x)));
                    return Result<List<FlightDTO>>.Ok(flights);
                case Failure<List<Flight>> failure:
                    return Result<List<FlightDTO>>.Fail(failure.Errors);
                default:
                    return Result<List<FlightDTO>>.Fail(new List<string>() {"An error occured"});
            }
        }
    }
    
}