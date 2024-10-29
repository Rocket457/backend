using crud.produto.connector;
using crud.produto.DAO;

var builder = WebApplication.CreateBuilder(args);

// Configura o logging
builder.Services.AddLogging();

// Registra os servi�os DAO e Connector
builder.Services.AddScoped<ItemDAO>();
builder.Services.AddScoped<Connector>();

// Adiciona suporte para controladores
builder.Services.AddControllers();

var app = builder.Build();

// Habilita redirecionamento HTTPS
app.UseHttpsRedirection();

// Mapeia os controladores
app.MapControllers();

// Configura CORS
app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Executa a aplica��o
app.Run();
