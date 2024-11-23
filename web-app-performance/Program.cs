using web_app_repository;
using MongoDB.Driver;
using web_energy_domain;

var builder = WebApplication.CreateBuilder(args);

// Configuração do MongoSettings
builder.Services.AddSingleton(new MongoSettings
{
    ConnectionString = "mongodb://energia:1234@localhost:27017/?authSource=admin&authMechanism=SCRAM-SHA-256",
    DatabaseName = "EnergyMonitorDB"
});

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var mongoSettings = sp.GetRequiredService<MongoSettings>();
    return new MongoClient(mongoSettings.ConnectionString);
});

builder.Services.AddScoped<IConsumoRepository, ConsumoRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
