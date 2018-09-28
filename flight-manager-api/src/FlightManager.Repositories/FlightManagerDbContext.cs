using System;
using FlightManager.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FlightManager.Repositories
{
    public class FlightManagerDbContext : DbContext
    {
        public FlightManagerDbContext(DbContextOptions<FlightManagerDbContext> options)
            : base(options)
        {
            AddData();
        }
 
        public DbSet<Flight> Flight { get; set; }
 
        private void AddData()
        {
            if (Flight.Any()) return;
            
            var flight = new Flight()
            {    
                Distance = 12524.661452383418,
                FuelNeeded = 250513.22904766834,
                DepartureName = "Paris",
                DepartureLatitude = 48.8534,
                DepartureLongitude = 2.3488,
                DepartureTime = DateTime.Now,
                ArrivalName = "NewYork",
                ArrivalLatitude = -73.968565,
                ArrivalLongitude = 40.779897,
                ArrivalTime = DateTime.Now.AddHours(8),
                Code = "Air-tonight",
                ConsumptionPerKm = 20,
                TakeOffEffort = 120,
                FlightTime = DateTime.Now - DateTime.Now.AddHours(8)
            };
            Flight.Add(flight);
 
            SaveChanges();
        }
    }
}