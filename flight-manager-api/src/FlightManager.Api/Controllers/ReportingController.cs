using System.Collections.Generic;
using System.Net;
using FlightManager.Api.Models;
using FlightManager.Libraries;
using FlightManager.Services;
using FlightManager.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class ReportingController : Controller
    {
        private readonly IReportingService _service;

        public ReportingController(IReportingService service)
        {
            _service = service;
        }

        /// <summary>
        ///  Get the report of all the flights
        /// </summary>
        /// <returns><see cref="List{Report}"/></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _service.GetReports();

            switch (result)
            {
                case Success<List<ReportDTO>> success:
                    var reports = new List<Report>();
                    success.Value.ForEach(x => reports.Add(new Report(x)));
                    return Ok(reports);
                case Failure<List<ReportDTO>> failure:
                    return StatusCode((int) HttpStatusCode.InternalServerError, failure.Errors);
                default:
                    return StatusCode((int) HttpStatusCode.InternalServerError, "Unexpected error");
            }
        }
    }
}