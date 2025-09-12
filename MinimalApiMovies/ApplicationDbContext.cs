using Microsoft.EntityFrameworkCore;

namespace MinimalApiMovies
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
