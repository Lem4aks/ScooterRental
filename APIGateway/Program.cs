using Grpc.Net.Client;
using Grpc.Net.ClientFactory;
using RentalSession;
using ClientAccount;
using ScooterInventoryGrpc;
using APIGateway.Repositories;
using APIGateway.Interfaces.Repositories;
namespace APIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<IScooterRepository, ScooterRepository>();
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();

            builder.Services.AddGrpcClient<ClientService.ClientServiceClient>(o =>
            {
                o.Address = new Uri("https://localhost:5002");
            });

            builder.Services.AddGrpcClient<RentalSessionService.RentalSessionServiceClient>(o =>
            {
                o.Address = new Uri("https://localhost:5004");
            });

            builder.Services.AddGrpcClient<ScooterInventoryService.ScooterInventoryServiceClient>(o =>
            {
                o.Address = new Uri("https://localhost:5006");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseAuthorization();

            

            app.MapControllers();


            app.Run();
        }
    }
}
