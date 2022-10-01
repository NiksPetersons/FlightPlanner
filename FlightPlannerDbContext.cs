using Microsoft.EntityFrameworkCore;

namespace Flight_planner
{
    public class FlightPlannerDbContext : DbContext
    {
        public FlightPlannerDbContext()
        {
        }

        public FlightPlannerDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=FlightPlanner.db");
        }
    }
}