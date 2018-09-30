using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FlightManager.Api.Models;
using FlightManager.Host;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NFluent;
using Xunit;

namespace FlightManager.IntegrationTests
{
    public class ReportingControllerTests
    {
        [Fact]
        public async Task Get_Should_Return_Ok_With_List()
        {
            // Arrange
            using (var server = new TestServer(new WebHostBuilder().UseStartup<Startup>()))
            {
                using (var client = server.CreateClient())
                {
                    // Act
                    var response = await client.GetAsync("api/v1/reporting");

                    //Assert
                    response.EnsureSuccessStatusCode();
                    Check.That(response.StatusCode == HttpStatusCode.OK);

                    var content = await response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}