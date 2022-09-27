using System;
using System.Collections.Generic;
using System.Linq;

namespace Flight_planner
{
    public class FlightList
    {
        private static List<Flight> _flights = new List<Flight>();
        private static int _id = 0;
        private static object _balanceLock = new object();

        public static Flight AddFlight(Flight flight)
        {
            lock (_balanceLock)
            {
                flight.Id = ++_id;
                _flights.Add(flight);
                return flight;
            }
        }

        public static Flight GetFlight(int id)
        {
            return _flights.FirstOrDefault(x => x.Id == id);
        }

        public static bool DoesFlightExist(Flight flight)
        {
            lock (_balanceLock)
            {
                foreach (Flight f in _flights)
                {
                    if (flight.ArrivalTime == f.ArrivalTime
                        && flight.DepartureTime == f.DepartureTime
                        && flight.Carrier == f.Carrier
                        && flight.To.City == f.To.City
                        && flight.From.City == f.From.City
                        && flight.To.AirportCode == f.To.AirportCode
                        && flight.From.AirportCode == f.From.AirportCode
                        && flight.To.Country == f.To.Country
                        && flight.From.Country == f.From.Country)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public static void Clear()
        {
            _flights.Clear();
            _id = 0;
        }

        public static bool FlightValidation(Flight flight)
        {
            lock (_balanceLock)
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
                {
                    return false;
                }

                return true;
            }
        }

        public static void DeleteFlightById(int id)
        {
            lock (_balanceLock)
            {
                int flightId = -1;
                flightId = _flights.FindIndex(x => x.Id == id);

                if (flightId != -1)
                {
                    _flights.Remove(_flights.First(x => x.Id == id));
                }
            }
        }

        public static Airport[] GetAirport(string search)
        {
            search = search.Trim().ToLower();
            Airport[] array = _flights
                .Where(x => x.From.City.ToLower().Contains(search) || x.From.AirportCode.ToLower().Contains(search) || x.From.Country.ToLower().Contains(search))
                .Select(x => x.From).ToArray();
            return array;
        }

        public static PageResults SearchFlights(SearchFlightsRequest request)
        {
            lock (_balanceLock)
            {
                var flights = _flights.Where(x => x.From.AirportCode.Trim().ToLower() == request.From.Trim().ToLower()
                                                  && x.To.AirportCode.Trim().ToLower() == request.To.Trim().ToLower()
                                                  && x.DepartureTime.Substring(0, 10) ==
                                                  request.DepartureDate).ToArray();
                return new PageResults(flights);
            }
        }
    }
}