using ClientService.Data;
using ClientService.Repositories;
using ClientService.Services;
using Microsoft.EntityFrameworkCore;

namespace ClientService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddGrpc();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddDbContext<ClientDbContext>(
                options =>
                {
                    options.UseNpgsql(configuration.GetConnectionString(nameof(ClientDbContext)));
                }
                );
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<ClientServiceImpl>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}