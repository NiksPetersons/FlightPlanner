using FlightPlanner_Core.Models;
using System;

namespace FlightPlanner_Core.Validations
{
    public class AirportValidator : IFlightValidator
    {
        public bool IsValid(Flight flight)
        {
            if (flight?.From == null
                || flight?.To == null
                || String.IsNullOrEmpty(flight?.From.Country)
                || String.IsNullOrEmpty(flight.From.City)
                || String.IsNullOrEmpty(flight.From.AirportCode)
                || String.IsNullOrEmpty(flight.To.Country)
                || String.IsNullOrEmpty(flight.To.City)
                || String.IsNullOrEmpty(flight.To.AirportCode)
                || flight.From.AirportCode.ToLower().Trim() == flight.To.AirportCode.ToLower().Trim())
            {
                return false;
            }

            return true;
        }
    }
}