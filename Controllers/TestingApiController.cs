using Microsoft.AspNetCore.Mvc;

namespace Flight_planner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingApiController : ControllerBase
    {
        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            FlightList.Clear();
            return Ok();
        }
    }
}
