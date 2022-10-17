using FlightPlanner_Core.Models;

namespace FlightPlanner_Core.Validations
{
    public interface IFlightValidator
    {
        bool IsValid(Flight flight);
    }
}