using MinimalApiMovies.Entities;

namespace MinimalApiMovies.Repositorys.Interface
{
    public interface IRepositoryActores
    {
        Task<int> CreateActor(Actor actor);
        Task DeleteActor(int id);
        Task<bool> ExistActor(int id);
        Task<Actor> GetActorById(int id);
        Task<List<Actor>> GetAllActores();
        Task UpdateActor(Actor actor);
    }
}