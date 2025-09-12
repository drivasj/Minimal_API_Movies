using MinimalApiMovies.Entities;

namespace MinimalApiMovies.Repositorys.Interface
{
    public interface IRepositoryGeneros
    {
        Task<List<Genero>> GetAll();
        Task<Genero?> GetId(int id);
        Task<int> Create(Genero genero);
    }
}
