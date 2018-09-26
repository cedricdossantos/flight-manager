using System.Collections.Generic;
using FlightManager.Services.Models;

namespace FlightManager.Services
{
    public interface ITracker
    {

        bool AddFlight(Flight flight);

        void UpdateFlight(string code, FlightInformations informations);

        Flight GetFlight(string code);

        List<Flight> GetFlights();
    }
}