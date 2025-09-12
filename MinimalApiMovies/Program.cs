
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using MinimalApiMovies;
using MinimalApiMovies.Entities;

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
}).CacheOutput(c=>c.Expire(TimeSpan.FromSeconds(15)));

// End Middleware area 

app.Run();

//dotnet tool install --global dotnet-ef
