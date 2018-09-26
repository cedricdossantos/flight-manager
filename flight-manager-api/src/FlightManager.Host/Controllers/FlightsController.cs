using System.Collections.Generic;
using System.Net;
using FlightManager.Services;
using FlightManager.Services.Helpers;
using FlightManager.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace FlightManager.Host.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// All the action dealing with the flights
    /// </summary>
    [Route("api/v1/[controller]")]
    public class FlightsController : Controller
    {
        private readonly ITracker _tracker;

        public FlightsController(ITracker tracker)
        {
            _tracker = tracker;
        }
        
        /// <summary>
        /// Retrieve all the flights
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _tracker.GetFlights();

            switch (result)
            {
                case Success<List<Flight>> success:
                    return this.Ok(success.Value);
                case Failure<List<Flight>> failure:
                    return this.StatusCode((int) HttpStatusCode.InternalServerError, failure.Errors);
            }

            return this.StatusCode((int) HttpStatusCode.InternalServerError, "Unexpected error");
        }

        /// <summary>
        /// Retrieve a flight corresponding to a flight code
        /// </summary>
        /// <param name="flightCode"></param>
        /// <returns></returns>
        [HttpGet("{flightCode}")]
        public IActionResult GetById(string flightCode)
        {
            var result = _tracker.GetFlight(flightCode);

            switch (result)
            {
                case Success<Flight> success:
                    return this.Ok(success.Value);
                case Failure<Flight> failure:
                    return this.NotFound(failure.Errors);
            }

            return this.Ok();
        }

        /// <summary>
        /// Update an existing flight
        /// </summary>
        /// <param name="flightCode"></param>
        /// <param name="infos"></param>
        /// <returns></returns>
        [HttpPut("{flightCode}")]
        public IActionResult Put(string flightCode, [FromBody] FlightInformations infos)
        {
            var result = _tracker.UpdateFlight(flightCode, new FlightPlan(infos));
            switch (result)
            {
                case Success success:
                    return this.Ok(success.Message);
                case Failure failure:
                    return this.NotFound(failure.Errors);
            }

            return this.StatusCode((int) HttpStatusCode.InternalServerError, "Unexpected error");
        }

        /// <summary>
        /// Create a flight
        /// </summary>
        /// <param name="flightCode"></param>
        /// <param name="infos"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(string flightCode, [FromBody]FlightInformations infos)
        {
            var result = _tracker.AddFlight(new Flight(flightCode, infos));
            switch (result)
            {
                case Success success:
                    return this.Ok(success.Message);
                case Failure failure:
                    return this.BadRequest(failure.Errors);
            }

            return this.StatusCode((int) HttpStatusCode.InternalServerError, "Unexpected error");
        }
    }
}