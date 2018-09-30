using System;

namespace FlightManager.Libraries.Distance
{
    
    public interface IDistanceCalculator
    {
        /// <summary>
        /// Compare Two GPS positions and return the flying distance in Km
        /// </summary>
        /// <param name="departureLat"><see cref="departureLat"/></param>
        /// <param name="departureLon"><see cref="departureLon"/></param>
        /// <param name="arrivalLat"><see cref="arrivalLat"/></param>
        /// <param name="arrivalLon"><see cref="arrivalLon"/></param>
        /// <returns>Distance in KM</returns>
        double GetDistanceBetween(double departureLat,double departureLon, double arrivalLat, double arrivalLon);
    }
    
    public class DistanceCalculator : IDistanceCalculator
    {
        
        public double GetDistanceBetween(double departureLat,double departureLon, double arrivalLat, double arrivalLon)
        {
            var rlat1 = Math.PI * departureLat/180;
            var rlat2 = Math.PI * arrivalLat/180;
            var rlon1 = Math.PI * departureLon/180;
            var rlon2 = Math.PI * arrivalLon/180;
 
            var theta = departureLon - arrivalLon;
            var rtheta = Math.PI * theta/180;
 
            var dist = Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) * Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180/Math.PI;
            dist = dist * 60 * 1.1515;
 

            return dist * 1.609344;
        }
    }
}