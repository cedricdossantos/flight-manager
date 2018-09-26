using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FlightManager.Host;
using FlightManager.Services.Helpers;
using FlightManager.Services.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NFluent;
using Xunit;

namespace FlightManager.IntegrationTests
{
    public class FlightsControllerTests
    {
        [Fact]
        public async Task GetFlights_Should_Return_Ok_With_List()
        {
            using (var server = new TestServer(new WebHostBuilder().UseStartup<Startup>()))
            {
                using (var client = server.CreateClient())
                {
                    var response = await client.GetAsync("api/v1/flights");

                    response.EnsureSuccessStatusCode();
                    Check.That(response.StatusCode == HttpStatusCode.OK);

                    var content = await response.Content.ReadAsStringAsync();
                    var flights = JsonConvert.DeserializeObject<List<Flight>>(content);

                    Check.That(flights).IsInstanceOf<List<Flight>>();
                    Check.That(flights.Any()).IsTrue();
                }
            }
        }
        
        [Fact]
        public async Task GetFlightById_Should_Return_Ok_With_Asked_Flight()
        {
            using (var server = new TestServer(new WebHostBuilder().UseStartup<Startup>()))
            {
                using (var client = server.CreateClient())
                {
                    var flightCode = "AF-2345";
                    var response = await client.GetAsync($"api/v1/flights/{flightCode}");
 
                    response.EnsureSuccessStatusCode();
 
                    Check.That(response.StatusCode == HttpStatusCode.OK);
                    var content = await response.Content.ReadAsStringAsync();
                    var flight = JsonConvert.DeserializeObject<Flight>(content);
                    Check.That(flight).IsInstanceOf<Flight>();
                }
            }
        }
        
        [Fact]
        public async Task GetFlightById_Should_Return_NotFond()
        {
            using (var server = new TestServer(new WebHostBuilder().UseStartup<Startup>()))
            {
                using (var client = server.CreateClient())
                {
                    var response = await client.GetAsync("api/v1/flights/sdrerefg4vbe4");
 
 
                    Check.That(response.StatusCode == HttpStatusCode.NotFound);
                }
            }
        }
        
        [Fact]
        public async Task PostFlight_Should_Return_Created_With_Asked_Flight()
        {
            using (var server = new TestServer(new WebHostBuilder().UseStartup<Startup>()))
            {
                using (var client = server.CreateClient())
                {
                    var flight = new FlightInformations()
                    {
                        DepartureAirportName = "test-departure",
                        DepartureAirportLongitude = 1,
                        DepartureAirportLatitude = 1,
                        ArrivalAirportName = "test-arrival",
                        ArrivalAirportLongitude = 2,
                        ArrivalAirportLatitude = 2
                    };

                    var textFlight = JsonConvert.SerializeObject(new {flightCode = "test-post", infos = flight});
                    var content = new StringContent(textFlight, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("api/v1/flights", content);
 
                    response.EnsureSuccessStatusCode();
 
                    Check.That(response.StatusCode == HttpStatusCode.OK);
                }
            }
        }
        
        [Fact]
        public async Task UpdateFlight_Should_Return_Ok()
        {
            using (var server = new TestServer(new WebHostBuilder().UseStartup<Startup>()))
            {
                using (var client = server.CreateClient())
                {
                    
                    var flight = new FlightInformations()
                    {
                        DepartureAirportName = "test-departure",
                        DepartureAirportLongitude = 1,
                        DepartureAirportLatitude = 1,
                        ArrivalAirportName = "test-arrival",
                        ArrivalAirportLongitude = 2,
                        ArrivalAirportLatitude = 2
                    };

                    var textFlight = JsonConvert.SerializeObject(new {flightCode = "test-post", infos = flight});
                    var content = new StringContent(textFlight, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync("api/v1/flights/EZ-2345",content);
 
                    response.EnsureSuccessStatusCode();
 
                    Check.That(response.StatusCode == HttpStatusCode.OK);
                }
            }
        }
        
        [Fact]
        public async Task UpdateFlight_Should_Return_Not_Found()
        {
            using (var server = new TestServer(new WebHostBuilder().UseStartup<Startup>()))
            {
                using (var client = server.CreateClient())
                {
                    
                    var flight = new FlightInformations()
                    {
                        DepartureAirportName = "test-departure",
                        DepartureAirportLongitude = 1,
                        DepartureAirportLatitude = 1,
                        ArrivalAirportName = "test-arrival",
                        ArrivalAirportLongitude = 2,
                        ArrivalAirportLatitude = 2
                    };

                    var textFlight = JsonConvert.SerializeObject(new {flightCode = "test-post", infos = flight});
                    var content = new StringContent(textFlight, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync("api/v1/flights/EZ-2345",content);
 
 
                    Check.That(response.StatusCode == HttpStatusCode.NotFound);
                }
            }
        }
    }
}