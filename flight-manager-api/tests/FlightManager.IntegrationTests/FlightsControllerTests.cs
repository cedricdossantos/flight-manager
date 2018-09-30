using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FlightManager.Api.Models;
using FlightManager.Host;
using FlightManager.Services.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NFluent;
using Xunit;

namespace FlightManager.IntegrationTests
{
    public class FlightsControllerTests
    {
        [Fact]
        public async Task GetFlights_Should_Return_Ok_With_List()
        {
            // Arrange
            using (var server = new TestServer(new WebHostBuilder().UseStartup<Startup>()))
            {
                using (var client = server.CreateClient())
                {
                    // Act
                    var response = await client.GetAsync("api/v1/flights");

                    //Assert
                    response.EnsureSuccessStatusCode();
                    Check.That(response.StatusCode == HttpStatusCode.OK);

                    var content = await response.Content.ReadAsStringAsync();
                    var flights = JsonConvert.DeserializeObject<List<FlightInformations>>(content);

                    Check.That(flights).IsInstanceOf<List<FlightInformations>>();
                    Check.That(flights.Any()).IsTrue();
                }
            }
        }
        
        [Fact]
        public async Task GetFlightById_Should_Return_Ok_With_Asked_Flight()
        {
            // Arrange
            using (var server = new TestServer(new WebHostBuilder().UseStartup<Startup>()))
            {
                using (var client = server.CreateClient())
                {
                    var flightCode = "Air-tonight";
                    
                    // Act
                    var response = await client.GetAsync($"api/v1/flights/{flightCode}");
 
                    // Assert
                    response.EnsureSuccessStatusCode();
 
                    Check.That(response.StatusCode == HttpStatusCode.OK);
                    var content = await response.Content.ReadAsStringAsync();
                    var flight = JsonConvert.DeserializeObject<FlightInformations>(content);
                    Check.That(flight.Code).IsEqualTo(flightCode);
                }
            }
        }
        
        [Fact]
        public async Task GetFlightById_Should_Return_NotFond()
        {
            // Arrange
            using (var server = new TestServer(new WebHostBuilder().UseStartup<Startup>()))
            {
                using (var client = server.CreateClient())
                {
                    // Act
                    var response = await client.GetAsync("api/v1/flights/sdrerefg4vbe4");
 
                    // Assert
                    Check.That(response.StatusCode == HttpStatusCode.NotFound);
                }
            }
        }
        
        [Fact]
        public async Task PostFlight_Should_Return_Ok()
        {
            // Arrange
            using (var server = new TestServer(new WebHostBuilder().UseStartup<Startup>()))
            {
                using (var client = server.CreateClient())
                {
                    var textFlight =
                        "{ \"code\": \"string\", \"departureAirportName\": \"string\", \"departureAirportLatitude\": 0, \"departureAirportLongitude\": 0, \"consumptionPerKm\": 0, \"takeOffEffort\": 0, \"departureTime\": \"2018-09-28T00:28:44.339Z\", \"arrivalTime\": \"2018-09-28T00:28:44.339Z\", \"arrivalAirportName\": \"string\", \"arrivalAirportLatitude\": 0, \"arrivalAirportLongitude\": 0}";
                    var content = new StringContent(textFlight, Encoding.UTF8, "application/json");
                    
                    // Act
                    var response = await client.PostAsync("api/v1/flights", content);
 
                    // Assert
                    response.EnsureSuccessStatusCode();
 
                    Check.That(response.StatusCode == HttpStatusCode.OK);
                }
            }
        }
        
        [Fact]
        public async Task PostFlight_Should_Return_Conflict()
        {
            // Arrange
            using (var server = new TestServer(new WebHostBuilder().UseStartup<Startup>()))
            {
                using (var client = server.CreateClient())
                {
                    var textFlight =
                        "{ \"code\": \"Air-tonight\", \"departureAirportName\": \"string\", \"departureAirportLatitude\": 0, \"departureAirportLongitude\": 0, \"consumptionPerKm\": 0, \"takeOffEffort\": 0, \"departureTime\": \"2018-09-28T00:28:44.339Z\", \"arrivalTime\": \"2018-09-28T00:28:44.339Z\", \"arrivalAirportName\": \"string\", \"arrivalAirportLatitude\": 0, \"arrivalAirportLongitude\": 0}";
                    var content = new StringContent(textFlight, Encoding.UTF8, "application/json");
                    
                    // Act
                    var response = await client.PostAsync("api/v1/flights", content);
 
                    // Assert
                    Check.That(response.StatusCode == HttpStatusCode.Conflict);
                }
            }
        }
        
        [Fact]
        public async Task UpdateFlight_Should_Return_Ok()
        {
            //Arrange
            using (var server = new TestServer(new WebHostBuilder().UseStartup<Startup>()))
            {
                using (var client = server.CreateClient())
                {
                    var textFlight = "{ \"departureAirportName\": \"string\", \"departureAirportLatitude\": 1, \"departureAirportLongitude\": 0, \"consumptionPerKm\": 0, \"takeOffEffort\": 0, \"departureTime\": \"2018-09-28T00:40:20.820Z\", \"arrivalTime\": \"2018-09-28T00:40:20.820Z\", \"arrivalAirportName\": \"string\", \"arrivalAirportLatitude\": 0, \"arrivalAirportLongitude\": 0}";
                    var content = new StringContent(textFlight, Encoding.UTF8, "application/json");
                    
                    // Act
                    var response = await client.PutAsync("api/v1/flights/Air-tonight",content);
 
                    // Assert
                    response.EnsureSuccessStatusCode();
 
                    Check.That(response.StatusCode == HttpStatusCode.OK);
                }
            }
        }
        
        [Fact]
        public async Task UpdateFlight_Should_Return_Not_Found()
        {
            // Arrange
            using (var server = new TestServer(new WebHostBuilder().UseStartup<Startup>()))
            {
                using (var client = server.CreateClient())
                {
                    var textFlight = "{ \"departureAirportName\": \"string\", \"departureAirportLatitude\": 1, \"departureAirportLongitude\": 0, \"consumptionPerKm\": 0, \"takeOffEffort\": 0, \"departureTime\": \"2018-09-28T00:40:20.820Z\", \"arrivalTime\": \"2018-09-28T00:40:20.820Z\", \"arrivalAirportName\": \"string\", \"arrivalAirportLatitude\": 0, \"arrivalAirportLongitude\": 0}";
                    var content = new StringContent(textFlight, Encoding.UTF8, "application/json");
                    
                    // Act
                    var response = await client.PutAsync("api/v1/flights/Air-tomorrow",content);
 
                    // Assert
                    Check.That(response.StatusCode == HttpStatusCode.NotFound);
                }
            }
        }
    }
}