using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace FlightManager.Host.Controllers
{
    [Route("api/v1/[controller]")]
    public class FlightsController : Controller
    {
        // GET
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok();
        }
        
        [HttpGet("[flightCode]")]
        public IActionResult GetById([FromRoute]string flightCode)
        {
            return this.Ok();
        }
    }
}