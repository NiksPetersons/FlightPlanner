using System.Collections.Generic;
using System.Linq;
using Flight_planner;
using FlightPlanner_Core.Services;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public List<Airport> SearchAirports(string search)
        {
            search = search.Trim().ToLower();
            var airports = _context.Airports.Where(x => x.City.ToLower().Contains(search)
                                                        || x.AirportCode.ToLower().Contains(search)
                                                        || x.Country.ToLower().Contains(search)).ToList();

            return airports;
        }
    }
}