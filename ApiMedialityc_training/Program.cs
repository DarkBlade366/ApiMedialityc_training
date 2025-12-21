using ApiMedialityc_training.Data;
using ApiMedialityc_training.Features.Users.Handlers;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register services in the dependency container here
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFastEndpoints();

//Conection with postgreSQL
var conection = builder.Configuration.GetConnectionString("DbApiMedialityc");
builder.Services.AddDbContext<MedialitycDBContext>(options =>
    options.UseNpgsql(conection));

// Add Swagger
builder.Services.AddSwaggerDocument(config =>
{
    config.PostProcess = document =>
    {
        document.Info.Title = "Medialityc API";
        document.Info.Version = "v1";
        document.Info.Description = "API para gestión de usuarios y reservas";
    };
});


// Register Users handler
builder.Services.AddScoped<CreateUserHandler>();

//Build the application
var app = builder.Build();

// Middleware configuration
app.UseFastEndpoints();
app.UseSwaggerGen();

// Run the application
app.Run();
