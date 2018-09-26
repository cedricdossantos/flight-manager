using System;
using System.Collections.Generic;
using FlightManager.Services.Helpers;
using FlightManager.Services.Models;
using Xunit;
using NFluent;
using NSubstitute;

namespace FlightManager.Services.Tests
{
    public class FlightTrackerTests
    {
        [Fact]
        public void GetFlight_Should_Return_Flight()
        {
            var flightCode = "F-light";
            var clientMock = Substitute.For<IClient>();
            clientMock
                .SelectFlight(Arg.Any<string>())
                .ReturnsForAnyArgs(Result<Flight>.Ok(new Flight(flightCode, new FlightPlan(new Airport("a", 1, 5),
                        new Airport("b", 5, 5)))
                    )
                );

            var tracker = new FlightTracker(clientMock);

            var result = tracker.GetFlight(flightCode);

            switch (result)
            {
                case Success<Flight> success:
                    Check.That(success.Value).IsInstanceOf<Flight>();
                    Check.That(success.Value.Code).IsEqualTo(flightCode);
                    break;
                case Failure<Flight> failure:
                    throw new Exception("should not be failure");
            }
        }

        [Fact]
        public void GetFlights_Should_Return_Flights()
        {
            var flightsLit = new List<Flight>()
            {
                new Flight("F42", new FlightPlan(new Airport("b", 5, 5), new Airport("a", 1, 5))),
                new Flight("F-Light", new FlightPlan(new Airport("a", 1, 5), new Airport("b", 5, 5)))
            };

            var clientMock = Substitute.For<IClient>();
            clientMock.SelectFlights()
                .Returns(Result<List<Flight>>.Ok(flightsLit));

            var tracker = new FlightTracker(clientMock);

            var result = tracker.GetFlights();


            switch (result)
            {
                case Success<List<Flight>> success:
                    Check.That(success.Value).IsInstanceOf<List<Flight>>();
                    Check.That(success.Value.Count).IsEqualTo(2);
                    break;
                case Failure<List<Flight>> failure:
                    throw new Exception("should not be failure");
            }
        }

        [Fact]
        public void AddFlights_Should_Return_True()
        {
            var toAdd = new Flight("newFlight", new FlightPlan(new Airport("a", 1, 5), new Airport("b", 5, 5)));
            var clientMock = Substitute.For<IClient>();
            clientMock.CreateFlight(toAdd).Returns(Result.Ok("Flight created"));
            var tracker = new FlightTracker(clientMock);
            var result = tracker.AddFlight(toAdd);
            Check.That(result.IsSuccess()).IsTrue();

            var flightsLit = new List<Flight>()
            {
                new Flight("F42", new FlightPlan(new Airport("b", 5, 5), new Airport("a", 1, 5))),
                new Flight("F-Light", new FlightPlan(new Airport("a", 1, 5), new Airport("b", 5, 5)))
            };
            flightsLit.Add(toAdd);
            clientMock.SelectFlights().Returns(Result<List<Flight>>.Ok(flightsLit));

            var flightsResult = tracker.GetFlights();
            switch (flightsResult)
            {
                case Success<List<Flight>> success:
                    Check.That(success.Value).IsInstanceOf<List<Flight>>();
                    Check.That(success.Value.Count).IsEqualTo(3);
                    break;
                case Failure<List<Flight>> failure:
                    throw new Exception("should not be failure");
            }
        }

        [Fact]
        public void UpdateFlights_Should_UpdateFlight()
        {
            var flightCode = "F-Light";
            var newInfos = new FlightPlan(new Airport("a", 2, 5), new Airport("b", 5, 5));

            var clientMock = Substitute.For<IClient>();

            clientMock.UpdateFlight(flightCode, newInfos).Returns(Result.Ok("successfully updated"));
            var tracker = new FlightTracker(clientMock);

            var result = tracker.UpdateFlight(flightCode, newInfos);
            Check.That(result.IsSuccess()).IsTrue();

            clientMock.SelectFlight(flightCode)
                .Returns(
                    Result<Flight>.Ok(new Flight(flightCode, new FlightPlan(newInfos.Departure, newInfos.Arrival))));
            var flightResult = tracker.GetFlight(flightCode);

            switch (flightResult)
            {
                case Success<Flight> success:
                    Check.That(success.Value.Code).IsEqualTo(flightCode);
                    Check.That(success.Value.FlightPlan.Departure.Coordinate.Latitude).IsEqualTo(2);
                    Check.That(success.Value.FlightPlan.Departure.Coordinate.Longitude).IsEqualTo(5);
                    break;
                case Failure<Flight> failure:
                    throw new Exception("should not be failure");
            }
        }
    }
}