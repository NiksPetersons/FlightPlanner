using FlightPlanner_Core.Interfaces;

namespace FlightPlanner_Core.Models
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}