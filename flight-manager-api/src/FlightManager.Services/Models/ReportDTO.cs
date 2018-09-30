using System;
using FlightManager.Repositories.Models;

namespace FlightManager.Services.Models
{
    public class ReportDTO
    {
        public string Code { get; set;  }
        
        public string DepartureName { get; set; }
        
        public double DepartureLongitude { get; set; }
        
        public double DepartureLatitude { get; set; }
        
        public string ArrivalName { get; set; }
        
        public double ArrivalLongitude { get; set; }
        
        public double ArrivalLatitude { get; set; }
        
        public double Distance { get; set; }
        
        public double ConsumptionPerKm { get; set; }
        
        public double TakeOffEffort { get; set; }
        
        public DateTime DepartureTime { get; set; }
        
        public DateTime ArrivalTime { get; set; }
        
        public TimeSpan FlightTime { get; set; }
        
        public double FuelNeeded { get; set; }

        public ReportDTO(Flight flight)
        {
            Code = flight.Code;
            DepartureName = flight.DepartureName;
            DepartureLongitude = flight.DepartureLongitude;
            DepartureLatitude = flight.DepartureLatitude;
            DepartureTime = flight.DepartureTime;
            ArrivalName = flight.ArrivalName;
            ArrivalLongitude = flight.ArrivalLongitude;
            ArrivalLatitude = flight.ArrivalLatitude;
            ArrivalTime = flight.ArrivalTime;
            Distance = flight.Distance;
            FlightTime = flight.FlightTime;
            ConsumptionPerKm = flight.ConsumptionPerKm;
            TakeOffEffort = flight.TakeOffEffort;
            FuelNeeded = flight.FuelNeeded;
        }
    }
}