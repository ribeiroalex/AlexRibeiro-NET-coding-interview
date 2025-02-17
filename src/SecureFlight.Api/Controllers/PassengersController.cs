using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecureFlight.Api.Models;
using SecureFlight.Api.Utils;
using SecureFlight.Core.Entities;
using SecureFlight.Core.Interfaces;

namespace SecureFlight.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PassengersController(
    IService<Passenger> personService,
    IRepository<Flight> flightRepository,
    IRepository<Passenger> passengerRepository,
    IMapper mapper)
    : SecureFlightBaseController(mapper)
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PassengerDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> Get()
    {
        var passengers = await personService.GetAllAsync();
        return MapResultToDataTransferObject<IReadOnlyList<Passenger>, IReadOnlyList<PassengerDataTransferObject>>(passengers);
    }
    
    [HttpGet("/flights/{flightId:long}/passengers")]
    [ProducesResponseType(typeof(IEnumerable<PassengerDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> GetPassengersByFlight(long flightId)
    {
        var passengers = await personService.FilterAsync(p => p.Flights.Any(x => x.Id == flightId));
        return !passengers.Succeeded ?
            NotFound($"No passengers were found for the flight {flightId}") :
            MapResultToDataTransferObject<IReadOnlyList<Passenger>, IReadOnlyList<PassengerDataTransferObject>>(passengers);
    }

    [HttpPost("/flights/{flightId:long}/passengers")]
    public async Task<IActionResult> Postpassenger([FromRoute]long flightId,[FromBody]string id)
    {
        var flight = await flightRepository.GetByIdAsync(flightId);
        var passenger = await personService.FindAsync(id);

        if (flight != null && passenger != null)
        {
            flight.Passengers.Add(passenger);
            await flightRepository.SaveChangesAsync();

            return Ok("OK! Passanger added");
        }

        return NotFound($"No passengers were found for the flight {flightId}");

    }

    [HttpDelete("/flights/{flightid:long}/passengers")]
    public async Task<IActionResult> DeletePassenger([FromRoute]long flightid, [FromBody]string id)
    {
        var flight = await flightRepository.GetByIdAsync(flightid);
        var passenger = (await personService.FindAsync(id))?.Result;

        if (flight != null && passenger != null)
        {
            flight.Passengers.Remove(passenger);

            if (!passenger.Flights.Any())
            {
               await passengerRepository.DeleteAsync(passenger);
            }

            await flightRepository.SaveChangesAsync();

            return Ok("OK! Passanger removed from flight");
        }

        return BadRequest("Something went wrong!");
    }
}