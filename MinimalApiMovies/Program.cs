
using MinimalApiMovies.Entities;

var builder = WebApplication.CreateBuilder(args);

// Services area

// End services area


var app = builder.Build();

// Middleware area

app.MapGet("/", () => "Hello Worldd!");


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
        },        new Genero
        {
            Id = 3,
            Name = "Comedia",
        },
    };
    return generos;
});

// End Middleware area 

app.Run();
