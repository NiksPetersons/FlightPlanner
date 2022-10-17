using FlightPlanner_Core.Models;

namespace FlightPlanner_Core.Validations
{
    public class CarrierValidator : IFlightValidator
    {
        public bool IsValid(Flight flight)
        {
            if (string.IsNullOrEmpty(flight.Carrier))
            {
                return false;
            }

            return true;
        }
    }
}