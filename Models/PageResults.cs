namespace Flight_planner.Models
{
    public class PageResults
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public FlightRequest[] Items { get; set; }

        public PageResults(FlightRequest[] flights)
        {
            Page = 0;
            TotalItems = flights.Length;
            Items = flights;
        }
    }
}