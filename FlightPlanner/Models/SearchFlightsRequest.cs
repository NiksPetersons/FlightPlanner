namespace Flight_planner.Models
{
    public class SearchFlightsRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string DepartureDate { get; set; }

        public bool IsValid(SearchFlightsRequest request)
        {
            if (request == null
                || string.IsNullOrEmpty(request.DepartureDate)
                || string.IsNullOrEmpty(request.From)
                || string.IsNullOrEmpty(request.To)
                || request.From == request.To)
            {
                return false;
            }

            return true;
        }
    }
}