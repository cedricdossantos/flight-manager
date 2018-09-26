using GeoCoordinatePortable;

namespace FlightManager.Services.Models
{
    public class Airport
    {
        public string Name { get; }
        
        public GeoCoordinate Coordinate { get; }

        public Airport(string name, double latitude, double longitude)
        {
            Name = name;
            Coordinate = new GeoCoordinate(latitude, longitude,0,50,50,0,0);
        }
    }
}