using System.Collections.Generic;
using System.Linq;
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
        
        public bool AddFlight(Flight flight)
        {
            _flights.Add(flight);
            return true;
        }

        public void UpdateFlight(string code, FlightInformations informations)
        {
            var toReplace = _flights.FirstOrDefault(f => f.Code == code);
            var toAdd = new Flight(code, informations.Departure,informations.Arrival);

            _flights.Remove(toReplace);
            _flights.Add(toAdd);

        }

        public Flight GetFlight(string code)
        {
            return _flights.FirstOrDefault(f => f.Code == code);
        }

        public List<Flight> GetFlights()
        {
            return _flights;
        }
    }
}