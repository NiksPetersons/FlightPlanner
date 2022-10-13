using FlightPlanner_Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Flight_planner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingApiController : ControllerBase
    {
        //private readonly IFlightService _flightService;

        //public TestingApiController(IFlightService flightService)
        //{
        //    _flightService = flightService;
        //}
        private readonly IFlightPlannerDbContext _context;

        public TestingApiController(IFlightPlannerDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _context.Airports.RemoveRange(_context.Airports);
            _context.Flights.RemoveRange(_context.Flights);

            _context.SaveChanges();
            return Ok();
        }
    }
}
