using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FlightManager.Host;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
 
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
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
                    var response = await client.GetAsync("api/v1/flights/F-light");
 
                    response.EnsureSuccessStatusCode();
 
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
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
                    var response = await client.PostAsync("api/v1/flights",new StringContent("test"));
 
                    response.EnsureSuccessStatusCode();
 
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
            }
        }
        
        [Fact]
        public async Task UpdateFlight_Should_Return_Created_With_Asked_Flight()
        {
            using (var server = new TestServer(new WebHostBuilder().UseStartup<Startup>()))
            {
                using (var client = server.CreateClient())
                {
                    var response = await client.PutAsync("api/v1/flights/F-light", new StringContent("test"));
 
                    response.EnsureSuccessStatusCode();
 
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
            }
        }
    }
}