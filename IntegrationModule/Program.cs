using IntegrationModule.Models;
using IntegrationModule.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // This is to generate the Swagger JSON endpoint
    var swaggerSettings = builder.Configuration.GetSection("Swagger");

    // Specify the Swagger document version and title
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = swaggerSettings["Title"],
        Version = swaggerSettings["Version"]
    });

    // Add a security definition for JWT Bearer tokens
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    // Add a security requirement for JWT Bearer tokens to access protected endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configure JWT services
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Add JWT bearer authentication scheme to services collection
    .AddJwtBearer(o => {
        var jwtKey = builder.Configuration["JWT:Key"];
        var jwtKeyBytes = Encoding.UTF8.GetBytes(jwtKey);
        o.TokenValidationParameters = new TokenValidationParameters // Set the token validation parameters for the middleware
        { 
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(jwtKeyBytes), // Set the secret key used to sign the token
            ValidateLifetime = true,
        };
    });

builder.Services.Configure<ApiBehaviorOptions>(options => {
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddSingleton<IUserRepository, UserRepository>();

// *** EF connection string configuration usage - DI container registration (Program.cs) ***
builder.Services.AddDbContext<RwaMoviesContext>(options =>
{
    options.UseSqlServer("Name=ConnectionStrings:RwaMoviesDatabase");
});


// Configure CORS policy for the client application
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("http://localhost:5108", "http://localhost:5249")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});


var app = builder.Build();


// Use Swagger interface in both production and developmentenvironments
app.UseSwagger();
// Serve the Swagger UI
app.UseSwaggerUI(c =>
{
    var swaggerSettings = builder.Configuration.GetSection("Swagger");

    c.SwaggerEndpoint(swaggerSettings["Endpoint"], swaggerSettings["Title"]); // Set the Swagger endpoint URL and title
    c.RoutePrefix = swaggerSettings["RoutePrefix"]; // Set the route prefix for the Swagger UI
});

app.UseHttpsRedirection();

// Use authentication / authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Use CORS in the middleware pipeline
app.UseCors("AllowOrigin"); 

app.MapControllers();

// Enable static files middleware
app.UseStaticFiles();

app.Run();
