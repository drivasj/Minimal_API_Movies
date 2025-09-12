using Microsoft.EntityFrameworkCore;
using MinimalApiMovies.Entities;

namespace MinimalApiMovies
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genero>().Property(p => p.Name).HasMaxLength(50);
        }

        public DbSet<Genero> Generos { get; set; }
    }
}
