using System.Collections.Generic;
using Flight_planner;

namespace FlightPlanner_Core.Services
{
    public interface IAirportService : IEntityService<Airport>
    {
        List<Airport> SearchAirports(string search);
    }
}