using Aggregator.Services;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddScoped<IScooterService, ScootersService>()
    .AddScoped<IUserService, UserService>();

// Register gRPC clients
builder.Services.AddGrpcClient< ClientAccount.ClientService.ClientServiceClient>((services, options) =>
{
    options.Address = new Uri("https://localhost:7180");
});
builder.Services.AddGrpcClient<ScooterInventoryGrpc.ScooterInventoryService.ScooterInventoryServiceClient>((services, options) =>
{
    options.Address = new Uri("https://localhost:7130");
});
builder.Services.AddGrpcClient<RentalSession.RentalSessionService.RentalSessionServiceClient>((services, options) =>
{
    options.Address = new Uri("https://localhost:7291");
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();