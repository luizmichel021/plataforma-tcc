using plataformatcc.Service;
using Repositories.CustomerRepository;

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte a Controllers
builder.Services.AddControllers();

// Registra o Service no container de injeção de dependências
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddLogging();

var app = builder.Build();

// Adiciona os Controllers ao pipeline
app.MapControllers();

app.Run();