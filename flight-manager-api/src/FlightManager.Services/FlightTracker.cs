using System.Collections.Generic;
using System.Linq;
using FlightManager.Services.Helpers;
using FlightManager.Services.Models;

namespace FlightManager.Services
{
    public class FlightTracker : ITracker
    {
        private List<Flight> _flights = new List<Flight>();

        public FlightTracker()
        {
            
            _flights.Add(new Flight("F42", new Airport("b", 5, 5), new Airport("a", 1, 5)));
            _flights.Add(new Flight("F-Light", new Airport("a", 1, 5), new Airport("b", 5, 5)));
        }
        
        public Result AddFlight(Flight flight)
        {
            _flights.Add(flight);
            return Result.Ok($"Flight {flight.Code} successfully created");
        }

        public Result UpdateFlight(string code, FlightInformations flightInformations)
        {
            var toReplace = _flights.FirstOrDefault(f => f.Code == code);
            var toAdd = new Flight(code, flightInformations.Departure,flightInformations.Arrival);

            _flights.Remove(toReplace);
            _flights.Add(toAdd);
            return Result.Ok($"Flight { code} successfully updated ");
        }

        public Result<Flight> GetFlight(string code)
        {
            return Result<Flight>.Ok(_flights.FirstOrDefault(f => f.Code == code));
        }

        public Result<List<Flight>> GetFlights()
        {
            return Result<List<Flight>>.Ok(_flights);
        }
    }
}