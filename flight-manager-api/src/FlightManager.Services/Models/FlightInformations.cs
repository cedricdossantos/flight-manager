namespace FlightManager.Services.Models
{
    public class FlightInformations
    {
        public Airport Departure { get; }

        public Airport Arrival { get; }

        public double Distance { get; }

        public FlightInformations(Airport departure, Airport arrival)
        {
            Departure = departure;
            Arrival = arrival;
            Distance = Departure.Coordinate.GetDistanceTo(Arrival.Coordinate);
        }
    }
}