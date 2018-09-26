using System;
using System.Collections.Generic;
using System.Linq;
using FlightManager.Services;
using FlightManager.Services.Helpers;
using FlightManager.Services.Models;

namespace FlightManager.InMemoryClient
{
    public class InmemoryClient : IClient
    {
        private List<Flight> _flights = new List<Flight>();

        public InmemoryClient()
        {
            _flights.Add(new Flight("EZ=2345", new FlightPlan(new Airport("Paris", 5, 5), new Airport("London", 1, 5))));
            _flights.Add(new Flight("VU-9230", new FlightPlan(new Airport("London", 1, 5), new Airport("Paris", 5, 5))));
            _flights.Add(new Flight("AF-2345", new FlightPlan(new Airport("Paris", 5, 5), new Airport("London", 1, 5))));
            _flights.Add(new Flight("TP-9230", new FlightPlan(new Airport("London", 1, 5), new Airport("Paris", 5, 5))));
            _flights.Add(new Flight("TR-2345", new FlightPlan(new Airport("Paris", 5, 5), new Airport("London", 1, 5))));
            _flights.Add(new Flight("ZO-9230", new FlightPlan(new Airport("London", 1, 5), new Airport("Paris", 5, 5))));
            _flights.Add(new Flight("SA-2345", new FlightPlan(new Airport("Paris", 5, 5), new Airport("London", 1, 5))));
            _flights.Add(new Flight("CC-9230", new FlightPlan(new Airport("London", 1, 5), new Airport("Paris", 5, 5))));
        }
        
        public Result CreateFlight(Flight flight)
        {
            if (_flights.Any(f => f.Code == flight.Code))
                return Result.Fail(new List<string>(){$"A flight with the code {flight.Code} already exists"});
            _flights.Add(flight);
            return Result.Ok($"Flight {flight.Code} successfully created");
        }

        public Result UpdateFlight(string code, FlightPlan flightPlan)
        {
            var flight = _flights.FirstOrDefault(f => f.Code == code);
            if (flight == null)
                return Result.Fail(new List<string>(){$"Flight {code} not found"});
            var toReplace = _flights.FirstOrDefault(f => f.Code == code);
            var toAdd = new Flight(code, new FlightPlan(flightPlan.Departure,flightPlan.Arrival));

            _flights.Remove(toReplace);
            _flights.Add(toAdd);
            return Result.Ok($"Flight {code} successfully updated ");
        }

        public Result<Flight> SelectFlight(string code)
        {
            var flight = _flights.FirstOrDefault(f => f.Code == code);
            if (flight == null)
                return Result<Flight>.Fail(new List<string>(){$"Flight {code} not found"});
        return Result<Flight>.Ok(flight);
        }

        public Result<List<Flight>> SelectFlights()
        {
            return Result<List<Flight>>.Ok(_flights);
        }
    }
}