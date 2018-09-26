using System;

namespace FlightManager.Services.Models
{
    public class Flight
    {
        public string Code { get; }

        public FlightPlan FlightPlan { get; }

        public Flight(string code, FlightPlan flightPlan)
        {
            Code = code;
            FlightPlan = flightPlan;
        }
        
        public Flight(string code, FlightInformations infos )
        {
            Code = code;
            FlightPlan = new FlightPlan(infos);
        }

        public Flight()
        {
            
        }
    }
}