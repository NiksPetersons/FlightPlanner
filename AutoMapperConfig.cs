using AutoMapper;
using Flight_planner.Models;

namespace Flight_planner
{
    public class AutoMapperConfig
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AirportRequest, Airport>()
                    .ForMember(x => x.Id, options => options.Ignore())
                    .ForMember(x => x.AirportCode, opt => 
                        opt.MapFrom(x => x.Airport));
                cfg.CreateMap<Airport, AirportRequest>().ForMember(x => x.Airport,opt => 
                    opt.MapFrom(x => x.AirportCode));
                cfg.CreateMap<FlightRequest, Flight>();
                cfg.CreateMap<Flight, FlightRequest>();
            });

            config.AssertConfigurationIsValid(); //todo delete after

            return config.CreateMapper();
        }
    }
}