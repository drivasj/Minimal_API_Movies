using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MinimalApiMovies.DTOs.Actores;
using MinimalApiMovies.Entities;
using MinimalApiMovies.Repositorys.Interface;

namespace MinimalApiMovies.Endpoints
{
    public static class ActoresEndpoints
    {
        public static RouteGroupBuilder MapActores(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapPost("/", Create).DisableAntiforgery();
            return groupBuilder;
        } 

        public static async Task<Created<ActorDTO>> Create([FromForm] CreateActorDTO createActorDTO, 
            IRepositoryActores repositoryActores,
            IOutputCacheStore outputCacheStore,
            IMapper mapper
            )
        {
            var actor = mapper.Map<Actor>(createActorDTO);
            var id = await repositoryActores.CreateActor(actor);
            await outputCacheStore.EvictByTagAsync("actores-get", default);
            var actorDTO = mapper.Map<ActorDTO>(actor);
            return TypedResults.Created($"/actores/{id}", actorDTO);
        }
    }
}
