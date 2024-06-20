using Microsoft.EntityFrameworkCore;
using ScooterService.Entities;

namespace ScooterService.Data
{
    public class ScooterDbContext : DbContext 
    {
        public ScooterDbContext(DbContextOptions<ScooterDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<ScooterEntity> Scooters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder =>
            {
                builder.AddSimpleConsole();
            }));
        }
    }
}
