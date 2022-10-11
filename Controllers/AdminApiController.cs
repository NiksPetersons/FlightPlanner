using System;
using System.Linq;
using FlightPlanner_Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flight_planner.Controllers
{
    [Route("admin-api")]
    [ApiController, Authorize]
    public class AdminApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private static object _balanceLock = new object();

        public AdminApiController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            lock (_balanceLock)
            {
                var flight = _flightService.GetCompleteFlightById(id);

                if (flight == null)
                {
                    return NotFound();
                }

                return Ok(flight);
            }
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlights(Flight flight)
        {
            lock (_balanceLock)
            {
                //if (!Validations.FlightValidation(flight))
                //{
                //    return BadRequest();
                //}

                //if (Validations.DoesFlightExist(flight, _context))
                //{
                //    return Conflict();
                //}

                _flightService.Create(flight);
                return Created("", flight);
            }
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            lock (_balanceLock)
            {
                var flight = _flightService.GetCompleteFlightById(id);

                if (flight != null)
                {
                    _flightService.Delete(flight);
                }

                return Ok();
            }
        }
    }
}
