using System;
using System.ComponentModel.DataAnnotations;
using FlightManager.Services.Models;

namespace FlightManager.Api.Models
{
    public class UpdateFlightInformations
    {
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
        
        public FlightDTO ToDomainModel(string code)
        {
            return new FlightDTO(
                code,
                new AirportDTO(
                    this.DepartureAirportName,
                    this.DepartureAirportLatitude,
                    this.DepartureAirportLongitude
                ),
                new AirportDTO(
                    this.ArrivalAirportName,
                    this.ArrivalAirportLatitude,
                    this.ArrivalAirportLongitude
                ),
                this.ConsumptionPerKm,
                this.TakeOffEffort,
                this.DepartureTime,
                this.ArrivalTime);
        }
    }
}