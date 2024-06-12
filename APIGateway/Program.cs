using ClientService;
using Ocelot.Middleware;
using Ocelot.DependencyInjection;
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
            builder.Services.AddOcelot();
            builder.Services.AddSwaggerForOcelot(builder.Configuration);
            builder.Configuration.AddJsonFile("ocelot.json");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseSwaggerForOcelotUI(option =>
            {
                option.PathToSwaggerGenerator = "/swagger/docs";
            });

            app.MapControllers();

            app.UseOcelot();

            app.RunAsync();
        }
    }
}
