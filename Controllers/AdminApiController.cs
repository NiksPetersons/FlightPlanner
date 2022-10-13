using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Flight_planner.Models;
using FlightPlanner_Core.Services;
using FlightPlanner_Core.Validations;
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
        private readonly IEnumerable<IFlightValidator> _validators;
        private readonly IMapper _mapper;
        private static object _balanceLock = new object();

        public AdminApiController(IFlightService flightService, IEnumerable<IFlightValidator> validators, IMapper mapper)
        {
            _flightService = flightService;
            _validators = validators;
            _mapper = mapper;
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

                var response = _mapper.Map<FlightRequest>(flight);
                return Ok(response);
            }
        }

        [Route("flights")]
        [HttpPut]
        public IActionResult PutFlights(FlightRequest request)
        {
            lock (_balanceLock)
            {
                var flight = _mapper.Map<Flight>(request);

                if (!_validators.All(x => x.IsValid(flight)))
                {
                    return BadRequest();
                }

                if (_flightService.DoesFlightExist(flight))
                {
                    return Conflict();
                }

                _flightService.Create(flight);
                request = _mapper.Map<FlightRequest>(flight);

                return Created("", request);
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
