using System;
using System.Collections.Generic;
using System.Linq;
using FlightManager.Libraries;
using FlightManager.Libraries.Distance;
using FlightManager.Repositories;
using FlightManager.Repositories.Models;
using FlightManager.Services.Models;
using NFluent;
using NSubstitute;
using NSubstitute.Exceptions;
using Xunit;

namespace FlightManager.Services.Tests
{
    public class FlightServiceTests
    {
        [Fact]
        public void GetFlight_Should_Return_Success_With_Flight()
        {
            // Arrange
            var flightCode = "F-light";
            var flight = new Flight()
            {
                ArrivalLatitude = 0,
                ArrivalLongitude = 0,
                ArrivalName = "test",
                ArrivalTime = DateTime.Now.AddHours(1),
                Code = flightCode,
                ConsumptionPerKm = 2,
                DepartureLatitude = 1,
                DepartureLongitude = 2,
                DepartureTime = DateTime.Now,
                DepartureName = "test"
            };
            var repositoryMock = Substitute.For<IFlightRepository>();
            repositoryMock
                .SelectFlight(Arg.Any<string>())
                .ReturnsForAnyArgs(Result<Flight>.Ok(flight)
                );

            var tracker = new FlightService(repositoryMock);

            // Act
            var result = tracker.GetFlight(flightCode);

            // Assert
            switch (result)
            {
                case Success<FlightDTO> success:
                    Check.That(success.Value).IsInstanceOf<FlightDTO>();
                    Check.That(success.Value.Code).IsEqualTo(flightCode);
                    repositoryMock.ReceivedWithAnyArgs(1).SelectFlight(Arg.Any<string>());
                    break;
                case Failure<FlightDTO> failure:
                    throw new Exception("should not be failure");
                case NotFound<FlightDTO> notFound:
                    throw new Exception("should not be not found");
            }
        }

        [Fact]
        public void GetFlight_Should_Return_Not_Found_With_Error()
        {
            // Arrange
            var flightCode = "F-light";
            var errorMessage = "flight does not exist";
            var repositoryMock = Substitute.For<IFlightRepository>();
            repositoryMock
                .SelectFlight(Arg.Any<string>())
                .ReturnsForAnyArgs(Result<Flight>.NotFound(errorMessage));

            var tracker = new FlightService(repositoryMock);

            // Act
            var result = tracker.GetFlight(flightCode);

            // Assert
            switch (result)
            {
                case Success<FlightDTO> success:
                    throw new Exception("should not be success");
                case Failure<FlightDTO> failure:
                    throw new Exception("should not be failure");
                case NotFound<FlightDTO> notFound:
                    Check.That(notFound.Error).IsEqualTo(errorMessage);
                    repositoryMock.ReceivedWithAnyArgs(1).SelectFlight(Arg.Any<string>());
                    break;
            }
        }

        [Fact]
        public void GetFlight_Should_Return_Failure_With_Errors()
        {
            // Arrange
            var flightCode = "F-light";
            var errorMessages = new List<string>() {"test", "test"};
            var repositoryMock = Substitute.For<IFlightRepository>();
            repositoryMock
                .SelectFlight(Arg.Any<string>())
                .ReturnsForAnyArgs(Result<Flight>.Fail(errorMessages));

            var tracker = new FlightService(repositoryMock);

            // Act
            var result = tracker.GetFlight(flightCode);

            // Assert
            switch (result)
            {
                case Success<FlightDTO> success:
                    throw new Exception("should not be success");
                case Failure<FlightDTO> failure:
                    Check.That(failure.Errors.Count()).IsEqualTo(errorMessages.Count);
                    repositoryMock.ReceivedWithAnyArgs(1).SelectFlight(Arg.Any<string>());
                    break;
                case NotFound<FlightDTO> notFound:
                    throw new Exception("should not be not found");
            }
        }

        [Fact]
        public void GetFlights_Should_Return_Flights()
        {
            // Arrange
            var distanceCalcMock = Substitute.For<IDistanceCalculator>();
            distanceCalcMock
                .GetDistanceBetween(Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>())
                .ReturnsForAnyArgs(100);
            var flightDTOList = new List<FlightDTO>()
            {
                new FlightDTO(
                    "F42",
                    new AirportDTO("b", 5, 5),
                    new AirportDTO("a", 1, 5),
                    10,
                    10,
                    DateTime.Now,
                    DateTime.Now.AddHours(1),
                    distanceCalcMock
                )
            };

            var flightList = new List<Flight>()
            {
                new Flight()
                {
                    DepartureName = "b",
                    DepartureLatitude = 5,
                    DepartureLongitude = 5,
                    DepartureTime = DateTime.Now,
                    ArrivalName = "a",
                    ArrivalLatitude = 1,
                    ArrivalLongitude = 5,
                    ArrivalTime = DateTime.Now.AddHours(1),
                    Code = "F42",
                    ConsumptionPerKm = 10,
                    TakeOffEffort = 10
                }
            };
            var repositoryMock = Substitute.For<IFlightRepository>();
            repositoryMock.SelectFlights()
                .Returns(Result<List<Flight>>.Ok(flightList));

            var tracker = new FlightService(repositoryMock);

            // Act
            var result = tracker.GetFlights();

            // Assert
            switch (result)
            {
                case Success<List<FlightDTO>> success:
                    Check.That(success.Value).IsInstanceOf<List<FlightDTO>>();
                    Check.That(success.Value.Count).IsEqualTo(1);
                    repositoryMock.Received(1).SelectFlights();
                    break;
                case Failure<List<FlightDTO>> failure:
                    throw new Exception("should not be failure");
                case NotFound<List<FlightDTO>> notFound:
                    throw new Exception("should not be not found");
            }
        }

        [Fact]
        public void GetFlights_Should_Return_Success_With_Empty_List()
        {
            // Arrange
            var repositoryMock = Substitute.For<IFlightRepository>();
            repositoryMock.SelectFlights()
                .Returns(Result<List<Flight>>.Ok(new List<Flight>()));

            var tracker = new FlightService(repositoryMock);

            // Act
            var result = tracker.GetFlights();

            // Assert
            switch (result)
            {
                case Success<List<FlightDTO>> success:
                    Check.That(success.Value).IsInstanceOf<List<FlightDTO>>();
                    Check.That(success.Value.Count).IsEqualTo(0);
                    repositoryMock.Received(1).SelectFlights();
                    break;
                case Failure<List<FlightDTO>> failure:
                    throw new Exception("should not be failure");
                case NotFound<List<FlightDTO>> notFound:
                    throw new Exception("should not be not found");
            }
        }

        [Fact]
        public void AddFlights_Should_Return_Success()
        {
            // Arrange
            var distanceCalcMock = Substitute.For<IDistanceCalculator>();
            distanceCalcMock
                .GetDistanceBetween(Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>())
                .ReturnsForAnyArgs(100);
            
            var newDTOFlight = new FlightDTO(
                "newFlight",
                new AirportDTO("a", 1, 5),
                new AirportDTO("b", 5, 5),
                10,
                10,
                DateTime.Now,
                DateTime.Now.AddHours(1),
                distanceCalcMock
            );

            var repositoryMock = Substitute.For<IFlightRepository>();
            repositoryMock.CreateFlight(Arg.Any<Flight>()).Returns(Result.Ok("Flight created"));
            
            // Act
            var tracker = new FlightService(repositoryMock);
            var result = tracker.AddFlight(newDTOFlight);

            // Assert
            Check.That(result.IsSuccess()).IsTrue();
            repositoryMock.ReceivedWithAnyArgs(1).CreateFlight(Arg.Any<Flight>());
        }

        [Fact]
        public void AddFlights_Should_Return_Failure()
        {
            // Arrange
            var distanceCalcMock = Substitute.For<IDistanceCalculator>();
            distanceCalcMock
                .GetDistanceBetween(Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>())
                .ReturnsForAnyArgs(100);
            
            var newDTOFlight = new FlightDTO(
                "newFlight",
                new AirportDTO("a", 1, 5),
                new AirportDTO("b", 5, 5),
                10,
                10,
                DateTime.Now,
                DateTime.Now.AddHours(1),
                distanceCalcMock
            );
            var repositoryMock = Substitute.For<IFlightRepository>();
            repositoryMock.CreateFlight(Arg.Any<Flight>())
                .Returns(Result.Fail(new List<string>() {"failed to create the flight"}));
            var tracker = new FlightService(repositoryMock);

            // Act
            var result = tracker.AddFlight(newDTOFlight);

            // Assert
            switch (result)
            {
                case Success success:
                    throw new Exception("should not be success");
                case NotFound notFound:
                    throw new Exception("should not be not found");
                case Failure failure:
                    Check.That(failure.Errors.Any()).IsTrue();
                    repositoryMock.ReceivedWithAnyArgs(1).CreateFlight(Arg.Any<Flight>());
                    break;
            }
        }

        [Fact]
        public void AddFlights_Should_Return_Conflict()
        {
            // Arrange
            var distanceCalcMock = Substitute.For<IDistanceCalculator>();
            distanceCalcMock
                .GetDistanceBetween(Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>())
                .ReturnsForAnyArgs(100);
            
            var newDTOFlight = new FlightDTO(
                "newFlight",
                new AirportDTO("a", 1, 5),
                new AirportDTO("b", 5, 5),
                10,
                10,
                DateTime.Now,
                DateTime.Now.AddHours(1),
                distanceCalcMock
            );
            var repositoryMock = Substitute.For<IFlightRepository>();
            repositoryMock.CreateFlight(Arg.Any<Flight>())
                .Returns(Result.Conflict("failed to create the flight"));
            
            var tracker = new FlightService(repositoryMock);

            // Act
            var result = tracker.AddFlight(newDTOFlight);

            // Assert
            switch (result)
            {
                case Success success:
                    throw new Exception("should not be success");
                case NotFound notFound:
                    throw new Exception("should not be not found");
                case Failure failure:
                    Check.That(failure.Errors.Any()).IsTrue();
                    repositoryMock.ReceivedWithAnyArgs(1).CreateFlight(Arg.Any<Flight>());
                    break;
            }
        }

        [Fact]
        public void UpdateFlights_Should_Return_Success()
        {
            // Arrange
            var distanceCalcMock = Substitute.For<IDistanceCalculator>();
            distanceCalcMock
                .GetDistanceBetween(Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>())
                .ReturnsForAnyArgs(100);
            
            var flightDTO = new FlightDTO(
                "F-42",
                new AirportDTO("a", 1, 5),
                new AirportDTO("b", 5, 5),
                10,
                10,
                DateTime.Now,
                DateTime.Now.AddHours(1),
                distanceCalcMock
            );

            var repositoryMock = Substitute.For<IFlightRepository>();

            repositoryMock.UpdateFlight(Arg.Any<Flight>()).Returns(Result.Ok("successfully updated"));
            
            var tracker = new FlightService(repositoryMock);

            // Act
            var result = tracker.UpdateFlight(flightDTO);

            // Assert
            Check.That(result.IsSuccess()).IsTrue();
            repositoryMock.ReceivedWithAnyArgs(1).UpdateFlight(Arg.Any<Flight>());
        }

        [Fact]
        public void UpdateFlights_Should_Return_Not_Found()
        {
            // Arrange
            var distanceCalcMock = Substitute.For<IDistanceCalculator>();
            distanceCalcMock
                .GetDistanceBetween(Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>())
                .ReturnsForAnyArgs(100);
            var flightDTO = new FlightDTO(
                "F-42",
                new AirportDTO("a", 1, 5),
                new AirportDTO("b", 5, 5),
                10,
                10,
                DateTime.Now,
                DateTime.Now.AddHours(1),
                distanceCalcMock
            );


            var repositoryMock = Substitute.For<IFlightRepository>();

            var errorMessage = "flight not found";
            repositoryMock.UpdateFlight(Arg.Any<Flight>()).Returns(Result.NotFound(errorMessage));
            var tracker = new FlightService(repositoryMock);

            var result = tracker.UpdateFlight(flightDTO);
            
            switch (result)
            {
                case Success success:
                    throw new Exception("should not be success");
                case NotFound notFound:
                    Check.That(notFound.Error).IsEqualTo(errorMessage);
                    repositoryMock.ReceivedWithAnyArgs(1).UpdateFlight(Arg.Any<Flight>());
                    break;
                case Failure failure:
                    throw  new Exception("should not be failure");
            }
        }

    [Fact]
        public void UpdateFlights_Should_Return_Failure()
        {
            // Arrange
            var distanceCalcMock = Substitute.For<IDistanceCalculator>();
            distanceCalcMock
                .GetDistanceBetween(Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>(), Arg.Any<double>())
                .ReturnsForAnyArgs(100);
            var flightDTO = new FlightDTO(
                "F-42",
                new AirportDTO("a", 1, 5),
                new AirportDTO("b", 5, 5),
                10,
                10,
                DateTime.Now,
                DateTime.Now.AddHours(1),
                distanceCalcMock
            );


            var repositoryMock = Substitute.For<IFlightRepository>();

            repositoryMock.UpdateFlight(Arg.Any<Flight>()).Returns(Result.Fail(new List<string>(){"could not update the flight"}));
            var tracker = new FlightService(repositoryMock);

            var result = tracker.UpdateFlight(flightDTO);
            
            switch (result)
            {
                case Success success:
                    throw new Exception("should not be success");
                case NotFound notFound:
                    throw  new Exception("should not be not found");
                case Failure failure:
                    Check.That(failure.Errors.Any()).IsTrue();
                    repositoryMock.ReceivedWithAnyArgs(1).UpdateFlight(Arg.Any<Flight>());
                    break;
            }
        }
    }
}