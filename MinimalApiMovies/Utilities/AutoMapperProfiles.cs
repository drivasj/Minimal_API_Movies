using AutoMapper;
using MinimalApiMovies.DTOs;
using MinimalApiMovies.Entities;

namespace MinimalApiMovies.Utilities
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<CreateGeneroDTO, Genero>();
            CreateMap<Genero, GeneroDTO>();
        }
    }
}
