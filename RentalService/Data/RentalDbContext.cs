
using Microsoft.EntityFrameworkCore;
using RentalService.Entities;

namespace RentalService.Data
{
    public class RentalDbContext : DbContext
    {
        public RentalDbContext(DbContextOptions<RentalDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<SessionEntity> Sessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder =>
            {
                builder.AddSimpleConsole();
            }));
        }
    }
}
