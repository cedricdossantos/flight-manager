using System;
using System.Collections.Generic;
using System.Linq;
using FlightManager.Libraries;
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
                    break;
                case NotFound<FlightDTO> notFound:
                    throw new Exception("should not be not found");
            }
        }

        [Fact]
        public void GetFlights_Should_Return_Flights()
        {
            // Arrange
            var flightDTOList = new List<FlightDTO>()
            {
                new FlightDTO(
                    "F42",
                    new AirportDTO("b", 5, 5),
                    new AirportDTO("a", 1, 5),
                    10,
                    10,
                    DateTime.Now,
                    DateTime.Now.AddHours(1)
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
                    break;
                case Failure<List<FlightDTO>> failure:
                    throw new Exception("should not be failure");
            }
        }

        [Fact]
        public void AddFlights_Should_Return_Success()
        {
            // Arrange
            var flightDTOList = new List<FlightDTO>()
            {
                new FlightDTO(
                    "F42",
                    new AirportDTO("b", 5, 5),
                    new AirportDTO("a", 1, 5),
                    10,
                    10,
                    DateTime.Now,
                    DateTime.Now.AddHours(1)
                )
            };


            var newDTOFlight = new FlightDTO(
                "newFlight",
                new AirportDTO("a", 1, 5),
                new AirportDTO("b", 5, 5),
                10,
                10,
                DateTime.Now,
                DateTime.Now.AddHours(1)
            );

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

            var newFlight = new Flight()
            {
                DepartureName = "a",
                DepartureLatitude = 5,
                DepartureLongitude = 5,
                DepartureTime = DateTime.Now,
                ArrivalName = "a",
                ArrivalLatitude = 1,
                ArrivalLongitude = 5,
                ArrivalTime = DateTime.Now.AddHours(1),
                Code = "newFlight",
                ConsumptionPerKm = 10,
                TakeOffEffort = 10
            };

            var repositoryMock = Substitute.For<IFlightRepository>();
            repositoryMock.CreateFlight(Arg.Any<Flight>()).Returns(Result.Ok("Flight created"));

            repositoryMock
                .SelectFlights()
                .Returns(callInfo =>
                {
                    var tmp = flightList;
                    tmp.Add(newFlight);
                    return Result<List<Flight>>.Ok(tmp);
                });

            // Act
            var tracker = new FlightService(repositoryMock);
            var result = tracker.AddFlight(newDTOFlight);
            var flightsResult = tracker.GetFlights();

            // Assert
            Check.That(result.IsSuccess()).IsTrue();
            switch (flightsResult)
            {
                case Success<List<FlightDTO>> success:
                    Check.That(success.Value).IsInstanceOf<List<FlightDTO>>();
                    Check.That(success.Value.Count).IsEqualTo(2);
                    break;
                case Failure<List<FlightDTO>> failure:
                    throw new Exception("should not be failure");
            }
        }

        [Fact]
        public void AddFlights_Should_Return_Failure()
        {
            // Arrange
            var newDTOFlight = new FlightDTO(
                "newFlight",
                new AirportDTO("a", 1, 5),
                new AirportDTO("b", 5, 5),
                10,
                10,
                DateTime.Now,
                DateTime.Now.AddHours(1)
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
                    break;
            }
        }

        [Fact]
        public void UpdateFlights_Should_Return_Success()
        {
            // Arrange
            var flightCode = "F-42";
            var flightDTO = new FlightDTO(
                flightCode,
                new AirportDTO("a", 1, 5),
                new AirportDTO("b", 5, 5),
                10,
                10,
                DateTime.Now,
                DateTime.Now.AddHours(1)
            );

            var flight = new Flight()
            {
                DepartureName = "b",
                DepartureLatitude = 1,
                DepartureLongitude = 5,
                DepartureTime = DateTime.Now,
                ArrivalName = "a",
                ArrivalLatitude = 5,
                ArrivalLongitude = 5,
                ArrivalTime = DateTime.Now.AddHours(1),
                Code = "F-42",
                ConsumptionPerKm = 10,
                TakeOffEffort = 10
            };
            var repositoryMock = Substitute.For<IFlightRepository>();

            repositoryMock.UpdateFlight(Arg.Any<Flight>()).Returns(Result.Ok("successfully updated"));
            repositoryMock.SelectFlight(flightCode)
                .Returns(
                    Result<Flight>.Ok(flight));

            var tracker = new FlightService(repositoryMock);

            // Act
            var result = tracker.UpdateFlight(flightDTO);

            var flightResult = tracker.GetFlight(flightCode);

            // Assert
            Check.That(result.IsSuccess()).IsTrue();

            switch (flightResult)
            {
                case Success<FlightDTO> success:
                    Check.That(success.Value.Code).IsEqualTo(flightCode);
                    Check.That(success.Value.Departure.Coordinate.Latitude)
                        .IsEqualTo(flightDTO.Departure.Coordinate.Latitude);
                    Check.That(success.Value.Departure.Coordinate.Longitude)
                        .IsEqualTo(flightDTO.Departure.Coordinate.Longitude);
                    break;
                case Failure<FlightDTO> failure:
                    throw new Exception("should not be failure");
                case NotFound<FlightDTO> notFound:
                    throw new Exception("should not be not found");
            }
        }

        [Fact]
        public void UpdateFlights_Should_Return_Not_Found()
        {
            // Arrange
            var flightCode = "F-42";
            var flightDTO = new FlightDTO(
                flightCode,
                new AirportDTO("a", 1, 5),
                new AirportDTO("b", 5, 5),
                10,
                10,
                DateTime.Now,
                DateTime.Now.AddHours(1)
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
                    break;
                case Failure failure:
                    throw  new Exception("should not be failure");
            }
        }

    [Fact]
        public void UpdateFlights_Should_Return_Failure()
        {
            // Arrange
            var flightCode = "F-42";
            var flightDTO = new FlightDTO(
                flightCode,
                new AirportDTO("a", 1, 5),
                new AirportDTO("b", 5, 5),
                10,
                10,
                DateTime.Now,
                DateTime.Now.AddHours(1)
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
                    break;
            }
        }
    }
}