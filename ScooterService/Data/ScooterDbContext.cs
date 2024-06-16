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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var scooters = GenerateTestScooters(10);
            modelBuilder.Entity<ScooterEntity>().HasData(scooters);
        }
        private List<ScooterEntity> GenerateTestScooters(int count)
        {
            var scooters = new List<ScooterEntity>();
            var random = new Random();
            string[] models = { "XR-1000", "SpeedMaster", "UrbanGlide", "EcoRide", "TurboScoot" };

            for (int i = 0; i < count; i++)
            {
                var scooter = new ScooterEntity
                {
                    Id = Guid.NewGuid(),
                    Model = models[random.Next(models.Length)],
                    Status = random.Next(2) == 0,
                };

                scooters.Add(scooter);
            }

            return scooters;
        }

    }
}
