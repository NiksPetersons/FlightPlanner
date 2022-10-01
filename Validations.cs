using System;
using Microsoft.EntityFrameworkCore;

namespace Flight_planner
{
    public static class Validations
    {
        public static bool DoesFlightExist(Flight flight, FlightPlannerDbContext context)
        {
            foreach (Flight f in context.Flights.Include(x => x.From)
                         .Include(x => x.To))
                if (flight.From != null && flight.To != null 
                    && flight.ArrivalTime == f.ArrivalTime
                    && flight.DepartureTime == f.DepartureTime
                    && flight.Carrier == f.Carrier
                    && flight.To.City == f.To.City
                    && flight.From.City == f.From.City
                    && flight.To.AirportCode == f.To.AirportCode
                    && flight.From.AirportCode == f.From.AirportCode
                    && flight.To.Country == f.To.Country
                    && flight.From.Country == f.From.Country)
                    return true;

            return false;
        }

        public static bool FlightValidation(Flight flight)
        {
            if (flight.From == null
                || flight.To == null
                || String.IsNullOrEmpty(flight.From.Country)
                || String.IsNullOrEmpty(flight.From.City)
                || String.IsNullOrEmpty(flight.From.AirportCode)
                || String.IsNullOrEmpty(flight.To.Country)
                || String.IsNullOrEmpty(flight.To.City)
                || String.IsNullOrEmpty(flight.To.AirportCode)
                || String.IsNullOrEmpty(flight.Carrier)
                || String.IsNullOrEmpty(flight.DepartureTime)
                || String.IsNullOrEmpty(flight.ArrivalTime)
                || flight.From.AirportCode.ToLower().Trim() == flight.To.AirportCode.ToLower().Trim()
                || DateTime.Parse(flight.DepartureTime) >= DateTime.Parse(flight.ArrivalTime))
                return false;

            return true;
            
        }
    }
}