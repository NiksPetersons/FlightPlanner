using System.Text.Json.Serialization;
using FlightPlanner_Core.Models;

namespace Flight_planner
{
    public class Airport : Entity
    {
        public string Country { get; set; }
        public string City { get; set; }
        [JsonPropertyName("airport")]
        public string AirportCode { get; set; }
    }
}