using System;

namespace FlightManager.Services.Models
{
    public class Coordinate
    {
        public double Longitude { get; }
        
        public double Latitude { get; }

        public Coordinate(double latitude, double longitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}