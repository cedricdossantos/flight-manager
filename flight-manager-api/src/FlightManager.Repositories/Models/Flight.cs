using System;

namespace FlightManager.Repositories.Models
{
    public class Flight
    {
        public int Id { get; set; }
        
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
        
        
    }
}