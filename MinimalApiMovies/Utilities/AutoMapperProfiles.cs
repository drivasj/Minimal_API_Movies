using AutoMapper;
using MinimalApiMovies.DTOs.Actores;
using MinimalApiMovies.DTOs.Genero;
using MinimalApiMovies.Entities;

namespace MinimalApiMovies.Utilities
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles() 
        {
            //Mapping for Genero
            CreateMap<CreateGeneroDTO, Genero>();
            CreateMap<Genero, GeneroDTO>();

            //Mapping for Actor
            CreateMap<CreateActorDTO, Actor>().ForMember(x=>x.Foto, options => options.Ignore());

            CreateMap<Actor, ActorDTO>();
        }
    }
}
