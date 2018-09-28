using System;
using System.ComponentModel.DataAnnotations;
using FlightManager.Services.Models;

namespace FlightManager.Api.Models
{
    public class FlightInformations
    {
        [Required]
        public string Code { get; set; }
        
        [Required]
        public string DepartureAirportName { get; set; }
        
        [Required]
        public double DepartureAirportLatitude { get; set; }

        [Required]
        public double DepartureAirportLongitude { get; set; }
        
        [Required]
        public double ConsumptionPerKm { get; set; }
        
        [Required]
        public double TakeOffEffort { get; set; }
        
        [Required]
        public DateTime DepartureTime { get; set; }
        
        [Required]
        public DateTime ArrivalTime { get; set; }
        
        [Required]
        public string ArrivalAirportName { get; set; }
        
        [Required]
        public double ArrivalAirportLatitude { get; set; }

        [Required]
        public double ArrivalAirportLongitude { get; set; }

        public FlightDTO ToDomainModel()
        {
            return new FlightDTO(
                Code,
                new AirportDTO(
                    DepartureAirportName,
                    DepartureAirportLatitude,
                    DepartureAirportLongitude
                ),
                new AirportDTO(
                    ArrivalAirportName,
                    ArrivalAirportLatitude,
                    ArrivalAirportLongitude
                ),
                ConsumptionPerKm,
                TakeOffEffort,
                DepartureTime,
                ArrivalTime);
        }

        public FlightInformations()
        {
            
        }

        public FlightInformations(FlightDTO flight)
        {
            Code = flight.Code;
            DepartureTime = flight.DepartureTime;
            DepartureAirportName = flight.Departure.Name;
            DepartureAirportLatitude = flight.Departure.Coordinate.Latitude;
            DepartureAirportLongitude = flight.Departure.Coordinate.Longitude;
            ArrivalTime = flight.ArrivalTime;
            ArrivalAirportName = flight.Arrival.Name;
            ArrivalAirportLatitude = flight.Arrival.Coordinate.Latitude;
            ArrivalAirportLongitude = flight.Arrival.Coordinate.Longitude;
            ConsumptionPerKm = flight.ConsumptionPerKm;
            TakeOffEffort = flight.TakeOffEffort;
        }
    }
}