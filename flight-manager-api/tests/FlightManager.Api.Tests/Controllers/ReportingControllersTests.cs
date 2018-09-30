using System.Collections.Generic;
using FlightManager.Api.Controllers;
using FlightManager.Libraries;
using FlightManager.Services;
using FlightManager.Services.Models;
using Microsoft.AspNetCore.Mvc;
using NFluent;
using NSubstitute;
using Xunit;

namespace FlightManager.Api.Tests.Controllers
{
    public class ReportingControllersTests
    {
        [Fact]
        public void Get_Should_Return_Ok()
        {
            // Arrange
            var serviceMock = Substitute.For<IReportingService>();
            serviceMock.GetReports().Returns(Result<List<ReportDTO>>.Ok(new List<ReportDTO>()));
            // Act
            var sut = new ReportingController(serviceMock);
            var res = sut.Get();
            // Assert
            Check.That(res).IsInstanceOf<OkObjectResult>();
        }
    }
}