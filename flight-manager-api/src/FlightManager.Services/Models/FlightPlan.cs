namespace FlightManager.Services.Models
{
    public class FlightPlan
    {
        public Airport Departure { get; }

        public Airport Arrival { get; }

        public double Distance { get; }

        public FlightPlan(Airport departure, Airport arrival)
        {
            Departure = departure;
            Arrival = arrival;
            Distance = Departure.Coordinate.GetDistanceTo(Arrival.Coordinate);
        }
        
        public FlightPlan(FlightInformations flight)
        {
            Departure = new Airport(flight.DepartureAirportName, flight.DepartureAirportLatitude, flight.DepartureAirportLongitude);
            Arrival = new Airport(flight.ArrivalAirportName, flight.ArrivalAirportLatitude, flight.ArrivalAirportLongitude);
            Distance = Departure.Coordinate.GetDistanceTo(Arrival.Coordinate);
        }

        public FlightPlan()
        {
            
        }
    }
}