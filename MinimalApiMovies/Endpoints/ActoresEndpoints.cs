using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MinimalApiMovies.DTOs.Actores;
using MinimalApiMovies.Entities;
using MinimalApiMovies.Repositorys.Interface;
using MinimalApiMovies.Services.Interface;

namespace MinimalApiMovies.Endpoints
{
    public static class ActoresEndpoints
    {
        private static readonly string containerName = "actores";
        public static RouteGroupBuilder MapActores(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapPost("/", Create).DisableAntiforgery();
            return groupBuilder;
        } 

        public static async Task<Created<ActorDTO>> Create([FromForm] CreateActorDTO createActorDTO, 
            IRepositoryActores repositoryActores,
            IOutputCacheStore outputCacheStore,
            IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos
            )
        {
            var actor = mapper.Map<Actor>(createActorDTO);

            if(createActorDTO.Foto is not null)
            {
                var url = await almacenadorArchivos.Save(containerName, createActorDTO.Foto);
                actor.Foto = url;
            }


            var id = await repositoryActores.CreateActor(actor);
            await outputCacheStore.EvictByTagAsync("actores-get", default);
            var actorDTO = mapper.Map<ActorDTO>(actor);
            return TypedResults.Created($"/actores/{id}", actorDTO);
        }
    }
}
