using web_energy_repository;

var builder = WebApplication.CreateBuilder(args);

// Configurar MongoDB
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoSettings"));

// Injeção de dependência
builder.Services.AddScoped<IConsumoRepository, MongoConsumoRepository>();

// Serviços padrões
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
