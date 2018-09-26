using System.Collections.Generic;
using FlightManager.Services.Helpers;
using FlightManager.Services.Models;

namespace FlightManager.Services
{
    public interface IClient
    {

        Result CreateFlight(Flight flight);

        Result UpdateFlight(string code, FlightPlan flightPlan);

        Result<Flight> SelectFlight(string code);

        Result<List<Flight>> SelectFlights();
    }
}