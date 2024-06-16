using Ocelot.DependencyInjection;
using Ocelot.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Добавление контроллеров и Endpoints API Explorer
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Добавление Ocelot и SwaggerForOcelot
builder.Services.AddOcelot();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

// Загрузка конфигурации ocelot.json
builder.Configuration.AddJsonFile("ocelot.json");

WebApplication app = builder.Build();

// Настройка middleware
app.UseHttpsRedirection();
app.UseAuthorization();

// Настройка SwaggerForOcelot UI
app.UseSwaggerForOcelotUI(options =>
{
    options.PathToSwaggerGenerator = "/swagger/docs";
});

app.MapControllers();

// Запуск Ocelot
await app.UseOcelot();
await app.RunAsync();
