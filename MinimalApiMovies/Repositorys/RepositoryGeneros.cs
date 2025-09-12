using Microsoft.EntityFrameworkCore;
using MinimalApiMovies.Entities;
using MinimalApiMovies.Repositorys.Interface;

namespace MinimalApiMovies.Repositorys
{
    public class RepositoryGeneros : IRepositoryGeneros
    {
        private readonly ApplicationDbContext context;

        public RepositoryGeneros(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> Create(Genero genero)
        {
            context.Add(genero);
            await context.SaveChangesAsync();
            return genero.Id;
        }

        public async Task<List<Genero>> GetAll()
        {
            return await context.Generos.OrderBy(x=>x.Name).ToListAsync();
        }

        public Task<Genero?> GetId(int id)
        {
            return context.Generos.FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
