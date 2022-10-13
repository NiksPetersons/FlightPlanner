using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Flight_planner.Models;
using FlightPlanner.Data;
using FlightPlanner_Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Flight_planner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {

        //private readonly FlightPlannerDbContext _context;

        //public CustomerApiController(FlightPlannerDbContext context)
        //{
        //    _context = context;
        //}
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

            PageResults results = FlightList.SearchFlights(request, _context);
            return Ok(results);
        }

        //[Route("flights/{id}")]
        //[HttpGet]
        //public IActionResult GetFlight(int id)
        //{
        //    var flight = FlightList.GetFlight(id, _context);

        //    if (flight == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(flight);
        //}
    }
}
