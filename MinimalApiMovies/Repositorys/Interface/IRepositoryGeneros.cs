using MinimalApiMovies.Entities;

namespace MinimalApiMovies.Repositorys.Interface
{
    public interface IRepositoryGeneros
    {
        Task<List<Genero>> GetAll();
        Task<Genero?> GetId(int id);
        Task<int> Create(Genero genero);
        Task<bool> Exist(int id);
        Task Update(Genero genero);

        Task Delete(int id);
    }
}
