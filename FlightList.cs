using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Flight_planner
{
    public class FlightList
    {
        public static Flight GetFlight(int id, FlightPlannerDbContext context)
        {
            var flight = context.Flights
                .Include(x => x.From)
                .Include(x => x.To)
                .FirstOrDefault(x => x.Id == id);

            return flight;
        }

        public static Airport[] GetAirport(string search, FlightPlannerDbContext context)
        {
            search = search.Trim().ToLower();
            Airport[] array = context.Airports
                .Where(x => x.City.ToLower().Contains(search) || x.AirportCode.ToLower().Contains(search) || x.Country.ToLower().Contains(search))
                .ToArray();

            return array;
        }

        public static PageResults SearchFlights(SearchFlightsRequest request, FlightPlannerDbContext context)
        {
            var flights = context.Flights.Where(x =>
                x.From.AirportCode.Trim().ToLower() == request.From.Trim().ToLower()
                && x.To.AirportCode.Trim().ToLower() == request.To.Trim().ToLower()
                && x.DepartureTime.Substring(0, 10) ==
                request.DepartureDate).ToArray();

            return new PageResults(flights);
        }
    }
}