using System;

namespace FlightManager.Services.Models
{
    public class Flight
    {
        public string Code { get; }

        public Airport Departure { get; }

        public Airport Arrival { get; }

        public double Distance { get; }

        public Flight(string code, Airport departure, Airport arrival)
        {
            Code = code;
            Departure = departure;
            Arrival = arrival;
            Distance = Departure.Coordinate.GetDistanceTo(Arrival.Coordinate);
        }
    }
}