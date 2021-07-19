using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecureFlight.Api.Models;
using SecureFlight.Core.Entities;
using SecureFlight.Core.Interfaces;

namespace SecureFlight.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PassengersController : ControllerBase
    {
        private readonly IService<Passenger> _personService;

        public PassengersController(IService<Passenger> personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var passengers = (await _personService.GetAllAsync()).Result
                .Select(x => new PassengerDataTransferObject
                {
                    Email = x.Email,
                    FirstName = x.FirstName,
                    Id = x.Id,
                    LastName = x.LastName
                });

            return Ok(passengers);
        }
    }
}
