using System;
using FlightManager.Services.Models;

namespace FlightManager.Api.Models
{
    public class Report
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

        public Report(ReportDTO dto)
        {
            Code = dto.Code;
            DepartureName = dto.DepartureName;
            DepartureLongitude = dto.DepartureLongitude;
            DepartureLatitude = dto.DepartureLatitude;
            DepartureTime = dto.DepartureTime;
            ArrivalName = dto.ArrivalName;
            ArrivalLongitude = dto.ArrivalLongitude;
            ArrivalLatitude = dto.ArrivalLatitude;
            ArrivalTime = dto.ArrivalTime;
            Distance = dto.Distance;
            FlightTime = dto.FlightTime;
            ConsumptionPerKm = dto.ConsumptionPerKm;
            TakeOffEffort = dto.TakeOffEffort;
            FuelNeeded = dto.FuelNeeded;
        }
    }
}