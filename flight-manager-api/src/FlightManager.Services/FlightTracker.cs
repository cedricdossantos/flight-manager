using System.Collections.Generic;
using System.Linq;
using FlightManager.Services.Helpers;
using FlightManager.Services.Models;

namespace FlightManager.Services
{
    public class FlightTracker : ITracker
    {
        private readonly IClient _client;

        public FlightTracker(IClient client)
        {
            _client = client;
        }
        
        public Result AddFlight(Flight flight)
        {
            return _client.CreateFlight(flight);
        }

        public Result UpdateFlight(string code, FlightPlan infos)
        {
            return _client.UpdateFlight(code, infos);
        }

        public Result<Flight> GetFlight(string code)
        {
            return _client.SelectFlight(code);
        }

        public Result<List<Flight>> GetFlights()
        {
            return _client.SelectFlights();
        }
    }
}