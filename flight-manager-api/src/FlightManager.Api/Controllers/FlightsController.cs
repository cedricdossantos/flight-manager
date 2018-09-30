using System.Collections.Generic;
using System.Net;
using FlightManager.Api.Models;
using FlightManager.Libraries;
using FlightManager.Libraries.Distance;
using FlightManager.Services;
using FlightManager.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// All the action dealing with the flights
    /// </summary>
    [Route("api/v1/[controller]")]
    public class FlightsController : Controller
    {
        private readonly IFlightService _flightService;
        private readonly IDistanceCalculator _distanceCalculator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flightService"></param>
        public FlightsController(IFlightService flightService, IDistanceCalculator distanceCalculator)
        {
            _flightService = flightService;
            _distanceCalculator = distanceCalculator;
        }
        
        /// <summary>
        /// Retrieve all the flights
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _flightService.GetFlights();

            switch (result)
            {
                case Success<List<FlightDTO>> success:
                    var flights = new List<FlightInformations>();
                    success.Value.ForEach(x => flights.Add(new FlightInformations(x)));
                    return Ok(flights);
                case Failure<List<FlightDTO>> failure:
                    return StatusCode((int) HttpStatusCode.InternalServerError, failure.Errors);
                default:
                    return StatusCode((int) HttpStatusCode.InternalServerError, "Unexpected error");
            }
        }

        /// <summary>
        /// Retrieve a flight corresponding to a flight code
        /// </summary>
        /// <param name="flightCode"></param>
        /// <returns></returns>
        [HttpGet("{flightCode}")]
        public IActionResult GetByCode(string flightCode)
        {
            var result = _flightService.GetFlight(flightCode);

            switch (result)
            {
                case Success<FlightDTO> success:
                    return Ok(new FlightInformations(success.Value));
                case NotFound<FlightDTO> notFound:
                    return NotFound(notFound.Error);
                case Failure<FlightDTO> failure:
                    return StatusCode((int) HttpStatusCode.InternalServerError, string.Join(",",failure.Errors));
                default:
                    return StatusCode((int) HttpStatusCode.InternalServerError, "Unexpected error");
            }
        }

        /// <summary>
        /// Update an existing flight
        /// </summary>
        /// <param name="flightCode"></param>
        /// <param name="infos"></param>
        /// <returns></returns>
        [HttpPut("{flightCode}")]
        public IActionResult Put(string flightCode, [FromBody] UpdateFlightInformations infos)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result = _flightService.UpdateFlight(infos.ToDomainModel(flightCode, _distanceCalculator));
            switch (result)
            {
                case Success success:
                    return Ok(success.Message);
                case NotFound notFound:
                    return NotFound(notFound.Error);
                case Failure failure :
                    return StatusCode((int) HttpStatusCode.InternalServerError, string.Join(",",failure.Errors));
                default:
                    return StatusCode((int) HttpStatusCode.InternalServerError, "Unexpected error");
            }
        }

        /// <summary>
        /// Create a flight
        /// </summary>
        /// <param name="flightCode"></param>
        /// <param name="infos"></param>
        /// <returns>Ok if the flight is created, Internal Server Error</returns>
        [HttpPost]
        public IActionResult Post([FromBody]FlightInformations infos)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result = _flightService.AddFlight(infos.ToDomainModel(_distanceCalculator));
            switch (result)
            {
                case Success success:
                    return Ok(success.Message);
                case Conflict conflict:
                    return Conflict(conflict.Error);
                case Failure failure:
                    return StatusCode((int) HttpStatusCode.InternalServerError, string.Join(",",failure.Errors));
                default:
                    return StatusCode((int) HttpStatusCode.InternalServerError, "Unexpected error");
            }
        }
    }
}