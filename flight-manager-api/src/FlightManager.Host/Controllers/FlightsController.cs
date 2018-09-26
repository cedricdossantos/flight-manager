using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace FlightManager.Host.Controllers
{
    [Route("api/v1/[controller]")]
    public class FlightsController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok();
        }
        
        [HttpGet("{flightCode}")]
        public IActionResult GetById([FromRoute]string flightCode)
        {
            return this.Ok();
        }
        
        [HttpPut("{flightCode}")]
        public IActionResult Put([FromRoute]string flightCode)
        {
            return this.Ok();
        }
        
        [HttpPost]
        public IActionResult Post()
        {

            return this.Ok();
        }
    }
}