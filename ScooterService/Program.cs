using Microsoft.EntityFrameworkCore;
using ScooterService.Data;
using ScooterService.Repositories;
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
            builder.Services.AddScoped<IScooterRepository,  ScooterRepository>();
            builder.Services.AddGrpc();

            var app = builder.Build();

            app.UseRouting();
            app.UseGrpcWeb();
            app.MapGrpcService<ScooterServiceImpl>().EnableGrpcWeb();

            app.Run();
        }
    }
}