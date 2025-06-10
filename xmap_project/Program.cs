using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKeyString = jwtSettings["SecretKey"];

if (string.IsNullOrEmpty(secretKeyString))
{
    throw new ArgumentNullException(nameof(secretKeyString), "A SecretKey JWT não pode ser nula ou vazia. Verifique a configuração em appsettings.json.");
}
var secretKey = Encoding.ASCII.GetBytes(secretKeyString);
builder.Services.AddAuthentication(x
        =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    ) .AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});;
builder.Services.AddAuthorization();
builder.Services.AddScoped<IMapaService, MapaService>();
builder.Services.AddControllers();

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();