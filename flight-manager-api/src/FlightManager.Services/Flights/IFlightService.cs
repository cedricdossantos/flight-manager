using System.Collections.Generic;
using FlightManager.Libraries;
using FlightManager.Services.Models;

namespace FlightManager.Services
{
    public interface IFlightService
    {

        Result AddFlight(FlightDTO flightDto);

        Result UpdateFlight(FlightDTO flightDto);

        Result<FlightDTO> GetFlight(string code);

        Result<List<FlightDTO>> GetFlights();
    }
}