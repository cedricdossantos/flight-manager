using System.Collections.Generic;
using FlightManager.Services.Helpers;
using FlightManager.Services.Models;

namespace FlightManager.Services
{
    public interface ITracker
    {

        Result AddFlight(Flight flight);

        Result UpdateFlight(string code, FlightPlan infos);

        Result<Flight> GetFlight(string code);

        Result<List<Flight>> GetFlights();
    }
}