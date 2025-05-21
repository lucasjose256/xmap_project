using Microsoft.EntityFrameworkCore;
using xmap_project.Data;
using xmap_project.Services;
using xmap_project.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=meubanco;Username=eproc;Password=coritiba2025"));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()  
            .AllowAnyMethod()  
            .AllowAnyHeader();
    });
});
builder.Services.AddControllers();
builder.Services.AddScoped<IAtividadeService, AtividadeService>();
builder.Services.AddScoped<IProcessService, ProcessService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();