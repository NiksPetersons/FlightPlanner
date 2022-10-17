using System.Collections.Generic;
using Flight_planner;
using FlightPlanner_Core.Models;

namespace FlightPlanner_Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight GetCompleteFlightById(int id);
        bool DoesFlightExist(Flight flight);
        List<Flight> SearchFlights(string from, string to, string departureDate);
    }
}