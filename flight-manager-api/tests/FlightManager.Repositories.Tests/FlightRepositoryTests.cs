using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using FlightManager.Libraries;
using FlightManager.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NFluent;
using NSubstitute;
using Xunit;
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace FlightManager.Repositories.Tests
{
    public class FlightRepositoryTests
    {
        [Fact]
        public void CreateFlight_Should_Return_Success()
        {
            // Arrange
            var flight = new Flight()
            {
                ArrivalLatitude = 1,
                ArrivalLongitude = 1,
                ArrivalName = "test",
                ArrivalTime = DateTime.Now.AddHours(1),
                DepartureLatitude = 2,
                DepartureLongitude = 2,
                DepartureName = "test1",
                DepartureTime = DateTime.Now,
                Code = "test",
                ConsumptionPerKm = 10,
                TakeOffEffort = 10
            };
            var options = new DbContextOptionsBuilder<FlightManagerDbContext>()
                .UseInMemoryDatabase(databaseName: "inMemoryTestDb")
                .Options;
            
            using (var context = new FlightManagerDbContext(options))
            {
                // Act
                var repo = new FlightRepository(context);
                var res = repo.CreateFlight(flight);
            
                // Assert
                Check.That(res.IsSuccess()).IsTrue();
            }
        }
        
        [Fact]
        public void CreateFlight_Should_Return_Conflict()
        {
            
            // Arrange

            var flight = new Flight()
            {
                ArrivalLatitude = 1,
                ArrivalLongitude = 1,
                ArrivalName = "test",
                ArrivalTime = DateTime.Now.AddHours(1),
                DepartureLatitude = 2,
                DepartureLongitude = 2,
                DepartureName = "test1",
                DepartureTime = DateTime.Now,
                Code = "test",
                ConsumptionPerKm = 10,
                TakeOffEffort = 10
            };
            var options = new DbContextOptionsBuilder<FlightManagerDbContext>()
                .UseInMemoryDatabase(databaseName: "inMemoryTestDb")
                .Options;
            using (var context = new FlightManagerDbContext(options))
            {
                context.Flight.Add(flight);
                context.SaveChanges();
                // Act
                var repo = new FlightRepository(context);
                var res = repo.CreateFlight(flight);
            
                // Assert
                Check.That(res.IsConflict()).IsTrue();

                context.Flight.Remove(flight);
                context.SaveChanges();
            }
        }
        
        [Fact]
        public void CreateFlight_Should_Return_Failure()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<FlightManagerDbContext>()
                .UseInMemoryDatabase(databaseName: "inMemoryTestDb")
                .Options;

            using (var context = new FlightManagerDbContext(options))
            {
                // Act
                var repo = new FlightRepository(context);
                var res = repo.CreateFlight(null);

                // Assert
                Check.That(res.IsFailure()).IsTrue();
            }
        }
        
        [Fact]
        public void UpdateFlight_Should_Return_Success()
        {
            
            // Arrange

            var flight = new Flight()
            {
                ArrivalLatitude = 1,
                ArrivalLongitude = 1,
                ArrivalName = "test",
                ArrivalTime = DateTime.Now.AddHours(1),
                DepartureLatitude = 2,
                DepartureLongitude = 2,
                DepartureName = "test1",
                DepartureTime = DateTime.Now,
                Code = "test",
                ConsumptionPerKm = 10,
                TakeOffEffort = 10
            };
            var options = new DbContextOptionsBuilder<FlightManagerDbContext>()
                .UseInMemoryDatabase(databaseName: "inMemoryTestDb")
                .Options;

            using (var context = new FlightManagerDbContext(options))
            {
                context.Flight.Add(flight);
                context.SaveChanges();

                // Act
                var repo = new FlightRepository(context);
                var res = repo.UpdateFlight(flight);

                // Assert
                Check.That(res.IsSuccess()).IsTrue();
                context.Flight.Remove(flight);
                context.SaveChanges();
            }
        }
        
        [Fact]
        public void UpdateFlight_Should_Return_Not_Found()
        {
            // Arrange
            var flight = new Flight()
            {
                ArrivalLatitude = 1,
                ArrivalLongitude = 1,
                ArrivalName = "test",
                ArrivalTime = DateTime.Now.AddHours(1),
                DepartureLatitude = 2,
                DepartureLongitude = 2,
                DepartureName = "test1",
                DepartureTime = DateTime.Now,
                Code = "test",
                ConsumptionPerKm = 10,
                TakeOffEffort = 10
            };
            var options = new DbContextOptionsBuilder<FlightManagerDbContext>()
                .UseInMemoryDatabase(databaseName: "inMemoryTestDb")
                .Options;

            using (var context = new FlightManagerDbContext(options))
            {
                // Act
                var repo = new FlightRepository(context);
                var res = repo.UpdateFlight(flight);
            
                // Assert
                Check.That(res.IsNotFound()).IsTrue();
            }

            
            
        }
        
        [Fact]
        public void UpdateFlight_Should_Return_Failure()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<FlightManagerDbContext>()
                .UseInMemoryDatabase(databaseName: "inMemoryTestDb")
                .Options;

            using (var context = new FlightManagerDbContext(options))
            {
                // Act
                var repo = new FlightRepository(context);
                var res = repo.UpdateFlight(null);

                // Assert
                Check.That(res.IsFailure()).IsTrue();
            }
        }

        [Fact]
        public void SelectFlight_Should_Return_Success()
        {
            // Arrange
            var flight = new Flight()
            {
                ArrivalLatitude = 1,
                ArrivalLongitude = 1,
                ArrivalName = "test",
                ArrivalTime = DateTime.Now.AddHours(1),
                DepartureLatitude = 2,
                DepartureLongitude = 2,
                DepartureName = "test1",
                DepartureTime = DateTime.Now,
                Code = "test",
                ConsumptionPerKm = 10,
                TakeOffEffort = 10
            };
            var options = new DbContextOptionsBuilder<FlightManagerDbContext>()
                .UseInMemoryDatabase(databaseName: "inMemoryTestDb")
                .Options;

            using (var context = new FlightManagerDbContext(options))
            {
                context.Flight.Add(flight);
                context.SaveChanges();

                // Act
                var repo = new FlightRepository(context);
                var res = repo.SelectFlight(flight.Code);

                // Assert
                Check.That(res.IsSuccess()).IsTrue();
                context.Flight.Remove(flight);
                context.SaveChanges();
            }
        }
        
        [Fact]
        public void SelectFlight_Should_Return_Not_Found()
        {
            // Arrange
            var flight = new Flight()
            {
                ArrivalLatitude = 1,
                ArrivalLongitude = 1,
                ArrivalName = "test",
                ArrivalTime = DateTime.Now.AddHours(1),
                DepartureLatitude = 2,
                DepartureLongitude = 2,
                DepartureName = "test1",
                DepartureTime = DateTime.Now,
                Code = "test",
                ConsumptionPerKm = 10,
                TakeOffEffort = 10
            };
            var options = new DbContextOptionsBuilder<FlightManagerDbContext>()
                .UseInMemoryDatabase(databaseName: "inMemoryTestDb")
                .Options;

            using (var context = new FlightManagerDbContext(options))
            {
                // Act
                var repo = new FlightRepository(context);
                var res = repo.SelectFlight(flight.Code);
            
                // Assert
                Check.That(res.IsNotFound()).IsTrue();
            }

            
            
        }
        
        [Fact]
        public void SelectFlights_Should_Return_Success()
        {
            // Arrange
            var flight = new Flight()
            {
                ArrivalLatitude = 1,
                ArrivalLongitude = 1,
                ArrivalName = "test",
                ArrivalTime = DateTime.Now.AddHours(1),
                DepartureLatitude = 2,
                DepartureLongitude = 2,
                DepartureName = "test1",
                DepartureTime = DateTime.Now,
                Code = "test",
                ConsumptionPerKm = 10,
                TakeOffEffort = 10
            };
            var options = new DbContextOptionsBuilder<FlightManagerDbContext>()
                .UseInMemoryDatabase(databaseName: "inMemoryTestDb")
                .Options;

            using (var context = new FlightManagerDbContext(options))
            {
                context.Flight.Add(flight);
                context.SaveChanges();

                // Act
                var repo = new FlightRepository(context);
                var res = repo.SelectFlights();
                context.Flight.Remove(flight);
                context.SaveChanges();

                // Assert
                switch (res)
                {
                    case Success<List<Flight>> success:
                        break;
                    default:
                        throw new Exception("should be success");
                }
                
            }
        }


    }
}