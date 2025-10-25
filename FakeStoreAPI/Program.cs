using System.Text;
using FakeStoreAPI.Custom;
using FakeStoreAPI.Data;
using FakeStoreAPI.Interfaces;
using FakeStoreAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Para regristar los servicios que utilizamos
builder.Services.AddScoped<IProductos, ProductosServices>();
builder.Services.AddScoped<ICurrency, CurrencyServices>();
builder.Services.AddScoped<Utilities>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//Esto ayuda al swagger a entender que estamos usando JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FakeStoreAPI",
        Version = "v1",
        Description = "API de ejemplo con autenticación JWT"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Introduce el token JWT con el prefijo 'Bearer'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

//Con esto conectamos a la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//Configuración de la autenticación JWT
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).
AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false; 
    config.SaveToken = true; 
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true, 
        ValidateIssuer = false, 
        ValidateAudience = false, 
        ValidateLifetime = true, 
        ClockSkew = TimeSpan.Zero, 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)) // Clave para firmar y validar el token.
    };

});

//Configuración de CORS para que sea llamada de cualquier origen
builder.Services.AddCors(options =>
{
    options.AddPolicy("NewPolicy", app =>
    {
        app.AllowAnyOrigin() 
           .AllowAnyHeader() 
           .AllowAnyMethod(); 
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NewPolicy");

app.UseHttpsRedirection();

app.UseAuthentication(); // activa la autenticacion con JWT

app.UseAuthorization(); // verifica si el usuario tiene los permisos para poder acceder

app.MapControllers();

app.Run();
