
using Microsoft.AspNetCore.Cors;
using MinimalApiMovies.Entities;

var builder = WebApplication.CreateBuilder(args);
var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos");
// Services area

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
 

// End services area

var app = builder.Build();

// Middleware area

app.UseCors();

app.MapGet("/", [EnableCors(policyName: "libre")] () => "Hello Worldd!");

app.MapGet("/generos", () =>
{
    var generos = new List<Genero>
    {
        new Genero
        {
            Id = 1,
            Name = "Drama",
        },
        new Genero
        {
            Id = 2,
            Name = "Acción",
        }, 
        new Genero
        {
            Id = 3,
            Name = "Comedia",
        },
    };
    return generos;
});

// End Middleware area 

app.Run();
