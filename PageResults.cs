namespace Flight_planner
{
    public class PageResults
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public Flight[] Items { get; set; }

        public PageResults(Flight[] flights)
        {
            Page = 0;
            TotalItems = flights.Length;
            Items = flights;
        }
    }
}