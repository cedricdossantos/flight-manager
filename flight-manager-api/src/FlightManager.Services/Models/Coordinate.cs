using System;

namespace FlightManager.Services.Models
{
    public class Coordinate
    {
        public double Longitude { get; }
        
        public double Latitude { get; }

        public Coordinate(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
        
        /// <summary>
        /// Compare Two GPS positions and return the flying distance in Km
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public double GetDistanceTo(Coordinate other)
        {
            if (double.IsNaN(this.Latitude) || double.IsNaN(this.Longitude) || (double.IsNaN(other.Latitude) || double.IsNaN(other.Longitude)))
                throw new ArgumentException("Argument latitude or longitude is not a number");
            double d1 = this.Latitude * 0.0174532925199433;
            double num1 = this.Longitude * 0.0174532925199433;
            double d2 = other.Latitude * 0.0174532925199433;
            double num2 = other.Longitude * 0.0174532925199433 - num1;
            double d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
            return (6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3))))/1000;
        }
    }
}