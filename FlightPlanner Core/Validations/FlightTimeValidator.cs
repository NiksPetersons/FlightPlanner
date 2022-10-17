using FlightPlanner_Core.Models;
using System;

namespace FlightPlanner_Core.Validations
{
    public class FlightTimeValidator : IFlightValidator
    {
        public bool IsValid(Flight flight)
        {
            if (String.IsNullOrEmpty(flight?.DepartureTime)
                || String.IsNullOrEmpty(flight?.ArrivalTime)
                || DateTime.Parse(flight.DepartureTime) >= DateTime.Parse(flight.ArrivalTime))
            {
                return false;
            }

            return true;
        }
    }
}