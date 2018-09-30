using System;
using System.Collections.Generic;
using System.Linq;
using FlightManager.Libraries;
using FlightManager.Repositories.Models;

namespace FlightManager.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly FlightManagerDbContext _context;

        public FlightRepository(FlightManagerDbContext context)
        {
            _context = context;
        }

        public Result CreateFlight(Flight flight)
        {
            try
            {
                var tmp = _context.Flight.FirstOrDefault(f => f.Code == flight.Code);
                if (tmp != null)
                    return Result.Conflict($"Flight {flight.Code} already exists");
                _context.Flight.Add(flight);
                _context.SaveChanges();
                return Result.Ok("Flight successfully saved");
            }
            catch (Exception e)
            {
                return Result.Fail(new List<string>() {$"An error occured, {e.Message}"});
            }
        }

        public Result UpdateFlight(Flight flight)
        {
            try
            {
                var tmp = _context.Flight.FirstOrDefault(f => f.Code == flight.Code);
                if (tmp == null)
                    return Result.NotFound($"Flight { flight.Code} not found");
            
                var toUpdate = _context.Flight.First(f => f.Code == flight.Code);
                toUpdate.Distance = flight.Distance;
                toUpdate.DepartureLatitude = flight.DepartureLatitude;
                toUpdate.DepartureLongitude = flight.DepartureLongitude;
                toUpdate.DepartureName = flight.DepartureName;
                toUpdate.DepartureTime = flight.DepartureTime;
                toUpdate.ArrivalLatitude = flight.ArrivalLatitude;
                toUpdate.ArrivalLongitude = flight.ArrivalLongitude;
                toUpdate.ArrivalName = flight.ArrivalName;
                toUpdate.ArrivalTime = flight.ArrivalTime;
                toUpdate.ConsumptionPerKm = flight.ConsumptionPerKm;
                toUpdate.TakeOffEffort = flight.TakeOffEffort;
                toUpdate.FlightTime = flight.FlightTime;
                toUpdate.FuelNeeded = flight.FuelNeeded;

                _context.SaveChanges();
                
                return  Result.Ok($"Flight {flight.Code} successfully updated");
            }
            catch (Exception e)
            {
                return Result.Fail(new List<string>() {$"An error occured, {e.Message}"});
            }
        }

        public Result<Flight> SelectFlight(string code)
        {
            try
            {
                var flight = _context.Flight.FirstOrDefault(f => f.Code == code);
                if (flight == null)
                    return Result<Flight>.NotFound($"Flight {code} not found");
                return  Result<Flight>.Ok(flight);
            }
            catch (Exception e)
            {
                return Result<Flight>.Fail(new List<string>(){$"An error occured, {e.Message}"});
            }
        }

        public Result<List<Flight>> SelectFlights()
        {
            try
            {
                return Result<List<Flight>>.Ok(_context.Flight.Any()
                    ? _context.Flight.ToList()
                    : new List<Flight>());
            }
            catch (Exception e)
            {
                return Result<List<Flight>>.Fail(new List<string>() {$"An error occured,{e.Message}"});
            }
        }
    }
}