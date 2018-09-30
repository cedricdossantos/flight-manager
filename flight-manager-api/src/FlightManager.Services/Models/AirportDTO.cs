using FlightManager.Repositories.Models;

namespace FlightManager.Services.Models
{
    public class AirportDTO
    {
        public string Name { get; }
        
        public Coordinate Coordinate { get; }

        public AirportDTO(string name, double latitude, double longitude)
        {
            Name = name;
            Coordinate = new Coordinate(latitude, longitude);
        }

        public AirportDTO(Airport airport)
        {
            Name = airport.Name;
            Coordinate = new Coordinate(airport.Latitude, airport.Longitude);
        }
    }
}