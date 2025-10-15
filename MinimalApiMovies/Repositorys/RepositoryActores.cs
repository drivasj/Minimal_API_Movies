using Microsoft.EntityFrameworkCore;
using MinimalApiMovies.Entities;
using MinimalApiMovies.Repositorys.Interface;

namespace MinimalApiMovies.Repositorys
{
    public class RepositoryActores : IRepositoryActores
    {
        private readonly ApplicationDbContext context;

        public RepositoryActores(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Actor>> GetAllActores()
        {
            return await context.Actores.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Actor?> GetActorById(int id)
        {
            return await context.Actores.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> CreateActor(Actor actor)
        {
            context.Actores.Add(actor);
            await context.SaveChangesAsync();
            return actor.Id;
        }
        public async Task<bool> ExistActor(int id)
        {
            return await context.Actores.AnyAsync(x => x.Id == id);
        }

        public async Task UpdateActor(Actor actor)
        {
            context.Actores.Update(actor);
            await context.SaveChangesAsync();
        }

        public async Task DeleteActor(int id)
        {
            var actor = await GetActorById(id);
            if (actor != null)
            {
                context.Actores.Remove(actor);
                await context.SaveChangesAsync();
            }
        }
    }
}
