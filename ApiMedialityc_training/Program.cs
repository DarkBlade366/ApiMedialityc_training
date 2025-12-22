using ApiMedialityc_training.Data;
using ApiMedialityc_training.Features.Users.DTOs;
using ApiMedialityc_training.Features.Users.Handlers;
using ApiMedialityc_training.Features.Users.Queries;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ApiMedialityc_training.Features.Users.Endpoints;
using NSwag;
using ApiMedialityc_training.Features.Common;

var builder = WebApplication.CreateBuilder(args);

// Configuration JWT
var key = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("JWT Key not configured");
var issuer = builder.Configuration["Jwt:Issuer"]
    ?? throw new InvalidOperationException("JWT Issuer not configured");

// Add authentication with JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

// For role-based authorization
builder.Services.AddAuthorization();

// Register services in the dependency container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFastEndpoints(); // Registers all Endpoint<TRequest, TResponse> automatically

// Database connection with PostgreSQL
var connection = builder.Configuration.GetConnectionString("DbApiMedialityc");
builder.Services.AddDbContext<MedialitycDBContext>(options =>
    options.UseNpgsql(connection));


// Configure NSwag / Swagger
builder.Services.AddSwaggerDocument(config =>
{
    config.PostProcess = doc =>
    {
        doc.Info.Title = "Medialityc API";
        doc.Info.Version = "v1";
        doc.Info.Description = "API for managing users, resources, and reservations";
    };

    // JWT Authorization setup in Swagger UI
    config.AddSecurity("JWT", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme
    {
        Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = NSwag.OpenApiSecurityApiKeyLocation.Header,
        Description = "Enter 'Bearer {your JWT token}'"
    });

    // Apply JWT security to operations that require auth
    config.OperationProcessors.Add(
        new NSwag.Generation.Processors.Security.AspNetCoreOperationSecurityScopeProcessor("JWT"));
});

// Register Users handlers
builder.Services.AddScoped<CreateUserHandler>();
builder.Services.AddScoped<IQueryHandler<GetUsersQuery, PagedResponse<UserResponseDto>>,GetUsersHandler>();
builder.Services.AddScoped<UpdateUserHandler>();
builder.Services.AddScoped<LoginHandler>();
builder.Services.AddScoped<DeactivateUserHandler>();

// Build the application
var app = builder.Build();

// Middleware configuration
app.UseAuthentication();
app.UseAuthorization();

// Map endpoints and Swagger
app.UseFastEndpoints();

app.UseOpenApi();
app.UseSwaggerUi(); // Displays the Swagger UI

// Run the application
app.Run();


