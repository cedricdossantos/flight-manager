using System;
using System.Collections.Generic;
using FlightManager.Services.Helpers;
using FlightManager.Services.Models;
using Xunit;
using NFluent;

namespace FlightManager.Services.Tests
{
    public class FlightTrackerTests
    {
        [Fact]
        public void GetFlight_Should_Return_Flight()
        {
            var tracker = new FlightTracker();

            var flightCode = "F-Light";
            var result = tracker.GetFlight(flightCode);
            
            switch (result)
            {
                case Success<Flight> success:
                    Check.That(success.Value).IsInstanceOf<Flight>();
                    Check.That(success.Value.Code).IsEqualTo(flightCode);
                    break;
                case Failure<Flight> failure:
                    throw new Exception("should not be failure");
                    break;
            }
        }

        [Fact]
        public void GetFlights_Should_Return_Flights()
        {
            var tracker = new FlightTracker();

            var result = tracker.GetFlights();

            
            switch (result)
            {
                case Success<List<Flight>> success:
                    Check.That(success.Value).IsInstanceOf<List<Flight>>();
                    Check.That(success.Value.Count).IsEqualTo(2);
                    break;
                case Failure<List<Flight>> failure:
                    throw new Exception("should not be failure");
                    break;
            }
            
        }

        [Fact]
        public void AddFlights_Should_Return_True()
        {
            var tracker = new FlightTracker();

            var toAdd = new Flight("newFlight", new Airport("a", 1, 5), new Airport("b", 5, 5));
            var result = tracker.AddFlight(toAdd);

            Check.That(result.IsSuccess()).IsTrue();

            var flightsResult = tracker.GetFlights();
            switch (flightsResult)
            {
                case Success<List<Flight>> success:
                    Check.That(success.Value).IsInstanceOf<List<Flight>>();
                    Check.That(success.Value.Count).IsEqualTo(3);
                    break;
                case Failure<List<Flight>> failure:
                    throw new Exception("should not be failure");
                    break;
            }
            
        }

        [Fact]
        public void UpdateFlights_Should_UpdateFlight()
        {
            var tracker = new FlightTracker();

            var newInfos = new FlightInformations(new Airport("a", 2, 5), new Airport("b", 5, 5));
            var flightCode = "F-Light";

            var result = tracker.UpdateFlight(flightCode, newInfos);
            Check.That(result.IsSuccess()).IsTrue();
            
            var flightResult = tracker.GetFlight(flightCode);
            
            switch (flightResult)
            {
                case Success<Flight> success:
                    Check.That(success.Value.Code).IsEqualTo(flightCode);
                    Check.That(success.Value.Departure.Coordinate.Latitude).IsEqualTo(2);
                    Check.That(success.Value.Departure.Coordinate.Longitude).IsEqualTo(5);
                    break;
                case Failure<Flight> failure:
                    throw new Exception("should not be failure");
                    break;
            }
            
            
        }
    }
}