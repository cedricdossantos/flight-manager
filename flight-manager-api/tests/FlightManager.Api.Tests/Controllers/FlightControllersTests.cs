using System;
using System.Collections.Generic;
using FlightManager.Api.Controllers;
using FlightManager.Api.Models;
using FlightManager.Libraries;
using FlightManager.Services;
using FlightManager.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using NFluent;
using NSubstitute;

namespace FlightManager.Api.Tests.Controllers
{
    public class FlightControllersTests
    {
        [Fact]
        public void Get_Should_Return_Ok()
        {
            // Arrange
            var serviceMock = Substitute.For<IFlightService>();
            serviceMock.GetFlights().Returns(Result<List<FlightDTO>>.Ok(new List<FlightDTO>()));
            // Act
            var sut = new FlightsController(serviceMock);
            var res = sut.Get();
            // Assert
            Check.That(res).IsInstanceOf<OkObjectResult>();
        }
        
        [Fact]
        public void GetByCode_Should_Return_Ok()
        {
            // Arrange
            var dto = new FlightDTO(
                "test",
                new AirportDTO("test", 1,1),
                new AirportDTO("test", 3,3),
                10,
                10,
                DateTime.Now,
                DateTime.Now.AddHours(1));
            var serviceMock = Substitute.For<IFlightService>();
            serviceMock.GetFlight(Arg.Any<string>()).Returns(Result<FlightDTO>.Ok(dto));
            // Act
            var sut = new FlightsController(serviceMock);
            var res = sut.GetByCode("test");
            // Assert
            Check.That(res).IsInstanceOf<OkObjectResult>();
        }
        
        
        [Fact]
        public void GetByCode_Should_Return_Not_Found()
        {
            // Arrange
            var serviceMock = Substitute.For<IFlightService>();
            serviceMock.GetFlight(Arg.Any<string>()).Returns(Result<FlightDTO>.NotFound("not found"));
            // Act
            var sut = new FlightsController(serviceMock);
            var res = sut.GetByCode("test");
            // Assert
            Check.That(res).IsInstanceOf<NotFoundObjectResult>();
            
        }
        
        [Fact]
        public void Put_Should_Return_Ok()
        {
            // Arrange
            var flightInfos = new UpdateFlightInformations()
            {
                DepartureTime = DateTime.Now,
                DepartureAirportLatitude = 0,
                DepartureAirportLongitude = 0,
                DepartureAirportName = "test",
                ArrivalTime = DateTime.Now.AddHours(2),
                ArrivalAirportLatitude = 2,
                ArrivalAirportLongitude = 2,
                ArrivalAirportName = "test",
                ConsumptionPerKm = 10,
                TakeOffEffort = 10
            };
            var serviceMock = Substitute.For<IFlightService>();
            serviceMock.UpdateFlight(Arg.Any<FlightDTO>()).Returns(Result.Ok("ok"));
            // Act
            var sut = new FlightsController(serviceMock);
            var res = sut.Put("test", flightInfos);
            // Assert
            Check.That(res).IsInstanceOf<OkObjectResult>();
        }
        
        [Fact]
        public void Put_Should_Return_Not_Found()
        {
            var flightInfos = new UpdateFlightInformations()
            {
                DepartureTime = DateTime.Now,
                DepartureAirportLatitude = 0,
                DepartureAirportLongitude = 0,
                DepartureAirportName = "test",
                ArrivalTime = DateTime.Now.AddHours(2),
                ArrivalAirportLatitude = 2,
                ArrivalAirportLongitude = 2,
                ArrivalAirportName = "test",
                ConsumptionPerKm = 10,
                TakeOffEffort = 10
            };
            var serviceMock = Substitute.For<IFlightService>();
            serviceMock.UpdateFlight(Arg.Any<FlightDTO>()).Returns(Result.NotFound("ok"));
            // Act
            var sut = new FlightsController(serviceMock);
            var res = sut.Put("test", flightInfos);
            // Assert
            Check.That(res).IsInstanceOf<NotFoundObjectResult>();
            
        }
        
        public void Post_Should_Return_Ok()
        {
            // Arrange
            var flightInfos = new FlightInformations()
            {
                Code = "test",
                DepartureTime = DateTime.Now,
                DepartureAirportLatitude = 0,
                DepartureAirportLongitude = 0,
                DepartureAirportName = "test",
                ArrivalTime = DateTime.Now.AddHours(2),
                ArrivalAirportLatitude = 2,
                ArrivalAirportLongitude = 2,
                ArrivalAirportName = "test",
                ConsumptionPerKm = 10,
                TakeOffEffort = 10
            };
            var serviceMock = Substitute.For<IFlightService>();
            serviceMock.AddFlight(Arg.Any<FlightDTO>()).Returns(Result.Ok("ok"));
            // Act
            var sut = new FlightsController(serviceMock);
            var res = sut.Post(flightInfos);
            // Assert
            Check.That(res).IsInstanceOf<OkObjectResult>();
        }
        
        
        
    }
}