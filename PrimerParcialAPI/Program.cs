using PrimerParcialAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ----------------- Services -----------------

// Controladores
builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        // Para mantener los nombres de propiedades tal como en el modelo (sin camelCase forzado)
        opts.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Health checks (GET /ping)
builder.Services.AddHealthChecks();

// DbContext con Azure SQL (usa el connection string de appsettings.json o de Azure App Service)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ----------------- App -----------------
var app = builder.Build();

// Middleware de Swagger (debe estar accesible en producción según lo pide el parcial)
app.UseSwagger();
app.UseSwaggerUI();

// HTTPS redirection
app.UseHttpsRedirection();

// Authorization si lo necesitas (de momento lo dejamos por defecto)
app.UseAuthorization();

// Mapear controladores y healthcheck
app.MapControllers();
app.MapHealthChecks("/ping");

app.Run();