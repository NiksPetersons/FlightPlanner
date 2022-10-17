using Flight_planner;
using FlightPlanner_Core.Models;
using FlightPlanner_Core.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public Flight GetCompleteFlightById(int id)
        {
            return _context.Flights.Include(x => x.From)
                .Include(x => x.To)
                .SingleOrDefault(X => X.Id == id);
        }

        public bool DoesFlightExist(Flight flight)
        {
           return _context.Flights.Any(f => flight.ArrivalTime == f.ArrivalTime
                                      && flight.DepartureTime == f.DepartureTime
                                      && flight.Carrier == f.Carrier
                                      && flight.To.AirportCode == f.To.AirportCode
                                      && flight.From.AirportCode == f.From.AirportCode);
        }

        public List<Flight> SearchFlights(string from, string to, string departureDate)
        {
            return _context.Flights.Where(x =>
                x.From.AirportCode.Trim().ToLower() == from.Trim().ToLower()
                && x.To.AirportCode.Trim().ToLower() == to.Trim().ToLower()
                && x.DepartureTime.Substring(0, 10) == departureDate).ToList();
        }
    }
}