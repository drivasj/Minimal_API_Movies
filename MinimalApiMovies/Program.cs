using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MinimalApiMovies;
using MinimalApiMovies.Endpoints;
using MinimalApiMovies.Entities;
using MinimalApiMovies.Migrations;
using MinimalApiMovies.Repositorys;
using MinimalApiMovies.Repositorys.Interface;

var builder = WebApplication.CreateBuilder(args);
var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos");
// Services area

//SQL SERVER
builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer("name=DefaultConnection"));

// Enable Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(configuration =>
    {
        configuration.WithOrigins(origenesPermitidos).AllowAnyOrigin().AllowAnyMethod();  //  (Any website can communicate with us in any specific)
    });

    options.AddPolicy("libre", configuration =>
    {
        configuration.AllowAnyOrigin().AllowAnyOrigin().AllowAnyMethod();  //  (Any website can communicate with us in any way.);
    });
});

// Enable Cache
builder.Services.AddOutputCache();

// Swagger
builder.Services.AddEndpointsApiExplorer(); // Explora todos los EndPoinsts
builder.Services.AddSwaggerGen();

//Interfaces
builder.Services.AddScoped<IRepositoryGeneros, RepositoryGeneros>();

//Automapper
builder.Services.AddAutoMapper(typeof(Program));

// End services area
var app = builder.Build();

// Middleware area
if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseOutputCache();

app.MapGet("/", [EnableCors(policyName: "libre")] () => "Hello Worldd!");

app.MapGroup("/generos").MapGeneros();

//End Middleware area 

app.Run();

//dotnet tool install --global dotnet-ef
