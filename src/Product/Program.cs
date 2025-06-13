using Services;
using Repositories;

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte a Controllers
builder.Services.AddControllers();

// Registra o Service no container de injeção de dependências
builder.Services.AddScoped<Service>();
builder.Services.AddScoped<Repository>();
builder.Services.AddLogging();

var app = builder.Build();

// Adiciona os Controllers ao pipeline
app.MapControllers();

app.Run();