using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using MinimalApiMovies.Entities;
using MinimalApiMovies.Repositorys.Interface;

namespace MinimalApiMovies.Endpoints
{
    public static class GenerosEndpoints
    {
        public static RouteGroupBuilder MapGeneros(this RouteGroupBuilder group)
        {
            // Obtener todos los generos
            group.MapGet("/", GetAllGeneros).CacheOutput(c => c.Expire(TimeSpan.FromSeconds(60)).Tag("generos-get"));

            // Obtener un genero por Id
            group.MapGet("/{id:int}", GetGeneroId);

            // Crear un genero
            group.MapPost("/", CreateGenero);

            // Actualizar un genero
            group.MapPut("/{id:int}", UpdateGenero);

            // Eliminar un genero
            group.MapDelete("/{id:int}", DeleteGenero);




            return group;
        }


        //Obtener todos los generos
        static async Task<Ok<List<Genero>>> GetAllGeneros(IRepositoryGeneros repository)
        {
            var generos = await repository.GetAll();
            return TypedResults.Ok(generos);
        }

        //Obtener un genero por Id
        static async Task<Results<Ok<Genero>, NotFound>> GetGeneroId(IRepositoryGeneros repository, int id)
        {
            var genero = await repository.GetId(id);

            if (genero is null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(genero);
        }

        //Crear un genero
        static async Task<Created<Genero>> CreateGenero(Genero genero, IRepositoryGeneros repository, IOutputCacheStore outputCacheStore)
        {
            var id = await repository.Create(genero);
            await outputCacheStore.EvictByTagAsync("generos-get", default); // Clean cache generos-get
            return TypedResults.Created($"/generos/{id}", genero);
        }

        //Actualizar Genero
        static async Task<Results<NoContent, NotFound>> UpdateGenero(int id, Genero genero, IRepositoryGeneros repository, IOutputCacheStore outputCacheStore)
        {
            var exist = await repository.Exist(id);

            if (!exist)
            {
                return TypedResults.NotFound();
            }

            await repository.Update(genero);
            await outputCacheStore.EvictByTagAsync("generos-get", default); // Clean cache generos-get
            return TypedResults.NoContent();
        }

        //Eliminar Genero
        static async Task<Results<NoContent, NotFound>> DeleteGenero(int id, IRepositoryGeneros repository, IOutputCacheStore outputCacheStore)
        {
            var exist = await repository.Exist(id);

            if (!exist)
            {
                return TypedResults.NotFound();
            }

            await repository.Delete(id);
            await outputCacheStore.EvictByTagAsync("generos-get", default); // Clean cache generos-get
            return TypedResults.NoContent();
        }
    }
}
