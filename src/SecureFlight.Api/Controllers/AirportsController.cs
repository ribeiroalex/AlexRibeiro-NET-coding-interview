using System;
using System.Collections;
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
    public class AirportsController : ControllerBase
    {
        private readonly IRepository<Airport> _airportRepository;
        private readonly IService<Airport> _airportService;

        public AirportsController(IRepository<Airport> airportRepository, IService<Airport> airportService)
        {
            _airportRepository = airportRepository;
            _airportService = airportService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AirportDataTransferObject>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
        public async Task<IActionResult> Get()
        {
            var airports = (await _airportService.GetAllAsync()).Result
                .Select(x => new AirportDataTransferObject
                {
                    City = x.City,
                    Code = x.Code,
                    Country = x.Country,
                    Name = x.Name
                });
            return Ok(airports);
        }

        [HttpPut]
        [ProducesResponseType(typeof(AirportDataTransferObject), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
        public IActionResult Put(AirportDataTransferObject airport)
        {
            var result = _airportRepository.Update(new Airport
            {
                City = airport.City,
                Code = airport.Code,
                Country = airport.Country,
                Name = airport.Name
            });

            return Ok(result);
        }
    }
}
