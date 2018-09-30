using System.Collections.Generic;
using FlightManager.Libraries;
using FlightManager.Repositories.Models;

namespace FlightManager.Repositories
{
    public interface IFlightRepository
    {
        Result CreateFlight(Flight flight);

        Result UpdateFlight(Flight flight);

        Result<Flight> SelectFlight(string code);

        Result<List<Flight>> SelectFlights();
    }
}