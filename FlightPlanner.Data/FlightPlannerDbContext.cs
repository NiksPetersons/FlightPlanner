using Flight_planner;
using FlightPlanner_Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FlightPlanner.Data
{
    public class FlightPlannerDbContext : DbContext, IFlightPlannerDbContext
    {
        public FlightPlannerDbContext()
        {
        }

        public FlightPlannerDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=FlightPlanner.db");
        }
    }
}