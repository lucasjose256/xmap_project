using Microsoft.EntityFrameworkCore;
using xmap_project.Data;
using xmap_project.Services;
using xmap_project.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte ao EF Core com PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=meubanco;Username=eproc;Password=coritiba2025"));

// Configuração de CORS - Adiciona política para permitir qualquer origem
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()  // Permite qualquer origem
            .AllowAnyMethod()  // Permite qualquer método HTTP (GET, POST, etc.)
            .AllowAnyHeader(); // Permite qualquer cabeçalho
    });
});

// Adiciona suporte a controllers
builder.Services.AddControllers();
builder.Services.AddScoped<IAtividadeService, AtividadeService>();
builder.Services.AddScoped<IProcessService, ProcessService>();

//builder.Services.AddScoped<IAtividadeService, AtividadeService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuração de CORS - Habilita o uso da política definida
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

// Mapeia os endpoints dos controllers (como LoginController)
app.MapControllers();

app.Run();