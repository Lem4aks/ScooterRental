using Grpc.Net.Client;
using Grpc.Net.ClientFactory;
using RentalSession;
using ClientAccount;
using ScooterInventoryGrpc;
using APIGateway.Repositories;
using APIGateway.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.CookiePolicy;
using APIGateway.JWT;
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
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SecretKey"]))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["cookies"];

                            return Task.CompletedTask;
                        }
                    };
                });

            builder.Services.AddAuthorization();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddScoped<IJwtProvider, JwtProvider>();    
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

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });

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
