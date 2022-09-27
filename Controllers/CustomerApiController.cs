using Microsoft.AspNetCore.Mvc;

namespace Flight_planner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        [Route("airports")]
        [HttpGet]
        public IActionResult GetAirport(string search)
        {
            Airport[] airports = FlightList.GetAirport(search);
            return Ok(airports);
        }

        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights(SearchFlightsRequest request)
        {
            if (!request.IsValid(request))
            {
                return BadRequest();
            }

            PageResults results = FlightList.SearchFlights(request);
            return Ok(results);
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightList.GetFlight(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}
