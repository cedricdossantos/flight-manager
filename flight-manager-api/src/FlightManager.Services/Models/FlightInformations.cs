namespace FlightManager.Services.Models
{
    public class FlightInformations
    {
        public string DepartureAirportName { get; set; }
        
        public double DepartureAirportLatitude { get; set; }

        public double DepartureAirportLongitude { get; set; }
        
        public string ArrivalAirportName { get; set; }
        
        public double ArrivalAirportLatitude { get; set; }

        public double ArrivalAirportLongitude { get; set; }

        public FlightInformations()
        {
            
        }
    }
}