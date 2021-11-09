using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureFlight.Api.Models;
using SecureFlight.Api.Utils;
using SecureFlight.Core.Entities;
using SecureFlight.Core.Interfaces;

namespace SecureFlight.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightsController : ControllerBase
    {
        private readonly IService<Flight> _flightService;

        public FlightsController(IService<Flight> flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FlightDataTransferObject>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
        public async Task<IActionResult> Get()
        {
            var flights = (await _flightService.GetAllAsync()).Result
                .Select(x => new FlightDataTransferObject
                {
                    Id = x.Id,
                    ArrivalDateTime = x.ArrivalDateTime,
                    Code = x.Code,
                    FlightStatusId = (int) x.FlightStatusId,
                    DepartureDateTime = x.DepartureDateTime,
                    DestinationAirport = x.DestinationAirport,
                    OriginAirport = x.OriginAirport
                });

            return Ok(flights);
        }
    }
}
