using Microsoft.EntityFrameworkCore;
using ScooterService.Data;
using ScooterService.Services;

namespace ScooterService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            builder.Services.AddDbContext<ScooterDbContext>(
                options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString(nameof(ScooterDbContext)));
                }
                );

            // Add services to the container.
            builder.Services.AddGrpc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}