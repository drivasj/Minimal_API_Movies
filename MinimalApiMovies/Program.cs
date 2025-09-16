using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using MinimalApiMovies;
using MinimalApiMovies.Entities;
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

// Obtener todos los generos
app.MapGet("/generos", async (IRepositoryGeneros repository) =>
{
    return await repository.GetAll();

}).CacheOutput(c=>c.Expire(TimeSpan.FromSeconds(60)).Tag("generos-get"));

// Obtener un genero por Id
app.MapGet("/generos/{id:int}", async (IRepositoryGeneros repository, int id) =>
{
    var genero = await repository.GetId(id);

    if(genero is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(genero);

});


// Crear un genero
app.MapPost("/generos", async (Genero genero, IRepositoryGeneros repository, IOutputCacheStore outputCacheStore) =>
{
    var id = await repository.Create(genero);
    await outputCacheStore.EvictByTagAsync("generos-get", default); // Clean cache generos-get
    return Results.Created($"/generos/{id}", genero);
});

// Actualizar un genero
app.MapPut("/generos/{id:int}", async (int id, Genero genero, IRepositoryGeneros repository, IOutputCacheStore outputCacheStore) =>
{
    var exist = await repository.Exist(id);

    if (!exist)
    {
        return Results.NotFound();
    }

    await repository.Update(genero);
    await outputCacheStore.EvictByTagAsync("generos-get", default); // Clean cache generos-get
    return Results.NoContent();
});

// Eliminar un genero
app.MapDelete("/generos/{id:int}", async (int id, IRepositoryGeneros repository, IOutputCacheStore outputCacheStore) =>
{
    var exist = await repository.Exist(id);

    if (!exist)
    {
        return Results.NotFound();
    }

    await repository.Delete(id);
    await outputCacheStore.EvictByTagAsync("generos-get", default); // Clean cache generos-get
    return Results.NoContent();

});


// End Middleware area 

app.Run();

//dotnet tool install --global dotnet-ef
