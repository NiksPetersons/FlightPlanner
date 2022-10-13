using AutoMapper;
using Flight_planner.Models;
using FlightPlanner_Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using FlightPlanner_Core.Models;

namespace Flight_planner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;

        public CustomerApiController(IFlightService flightService, IMapper mapper)
        {
            _flightService = flightService;
            _mapper = mapper;
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult GetAirport(string search)
        {
            List<Airport> airports = _flightService.SearchAirports(search);
            var airportRequests = _mapper.Map<List<Airport>,List<AirportRequest>>(airports);
            return Ok(airportRequests.ToArray());
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights(SearchFlightsRequest request)
        {
            if (!request.IsValid(request))
            {
                return BadRequest();
            }

            var flights = _flightService.SearchFlights(request.From, request.To, request.DepartureDate);
            var flightRequests = _mapper.Map<List<Flight>,List<FlightRequest>>(flights).ToArray();
            return Ok(new PageResults(flightRequests));
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = _flightService.GetCompleteFlightById(id);

            if (flight == null)
            {
                return NotFound();
            }

            var request = _mapper.Map<FlightRequest>(flight);
            return Ok(request);
        }
    }
}
