using System;
using FlightManager.Repositories.Models;

namespace FlightManager.Services.Models
{
    public class FlightDTO
    {
        public string Code { get; }

        public AirportDTO Departure { get; }

        public AirportDTO Arrival { get; }

        public double Distance { get; }

        public double ConsumptionPerKm { get; }

        public double TakeOffEffort { get; }

        public DateTime DepartureTime { get; }

        public DateTime ArrivalTime { get; }

        public TimeSpan FlightTime { get; }

        public double FuelNeeded { get; }


        public FlightDTO(string code, AirportDTO departure, AirportDTO arrival,
            double consumptionPerKm, double takeOffEffort, DateTime departureTime, DateTime arrivalTime)
        {
            Code = code;
            Departure = departure;
            Arrival = arrival;
            Distance = Departure.Coordinate.GetDistanceTo(Arrival.Coordinate);
            ConsumptionPerKm = consumptionPerKm;
            TakeOffEffort = takeOffEffort;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            FuelNeeded = ConsumptionPerKm * Distance + TakeOffEffort;
            FlightTime = ArrivalTime - DepartureTime;
        }

        public FlightDTO()
        {
        }

        public FlightDTO(Flight flight)
        {
            Code = flight.Code;
            Departure = new AirportDTO(
                flight.DepartureName,
                flight.DepartureLatitude,
                flight.DepartureLongitude);
            Arrival = new AirportDTO(
                flight.ArrivalName,
                flight.ArrivalLatitude,
                flight.ArrivalLongitude);
            Distance = flight.Distance;
            ConsumptionPerKm = flight.ConsumptionPerKm;
            TakeOffEffort = flight.TakeOffEffort;
            DepartureTime = flight.DepartureTime;
            ArrivalTime = flight.ArrivalTime;
            FlightTime = flight.FlightTime;
            FuelNeeded = flight.FuelNeeded;
        }

        public Flight ToDbModel()
        {
            return new Flight()
            {
                Code = this.Code,
                DepartureName = this.Departure.Name,
                DepartureLatitude = this.Departure.Coordinate.Latitude,
                DepartureLongitude = this.Departure.Coordinate.Longitude,
                ArrivalName = this.Arrival.Name,
                ArrivalLongitude = this.Arrival.Coordinate.Longitude,
                ArrivalLatitude = this.Arrival.Coordinate.Latitude,
                DepartureTime = this.DepartureTime,
                ArrivalTime = this.ArrivalTime,
                ConsumptionPerKm = this.ConsumptionPerKm,
                Distance = this.Distance,
                FlightTime = this.FlightTime,
                FuelNeeded = this.FuelNeeded,
                TakeOffEffort = this.TakeOffEffort
            };
        }
    }
}