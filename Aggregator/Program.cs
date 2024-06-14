using Aggregator.Services;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddScoped<IScooterService, ScootersService>()
    .AddScoped<IUserService, UserService>();

// Register gRPC clients
builder.Services.AddGrpcClient<ClientAccount.ClientService.ClientServiceBase>((services, options) =>
{
    options.Address = new Uri("https://localhost:7180"); // Обновите URI на правильный
});

builder.Services.AddGrpcClient<ScooterInventoryGrpc.ScooterInventoryService.ScooterInventoryServiceBase>((services, options) =>
{
    options.Address = new Uri("https://localhost:7130"); // Обновите URI на правильный
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