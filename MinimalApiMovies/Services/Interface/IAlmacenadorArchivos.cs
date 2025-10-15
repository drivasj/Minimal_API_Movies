namespace MinimalApiMovies.Services.Interface
{
    public interface IAlmacenadorArchivos
    {
        Task Delete(string? route, string container);
        Task<string> Save(string container, IFormFile file);
        async Task<string> Edit(string route, string container, IFormFile file)
        {
            // If the route is not null or empty, delete the existing file in azure
            await Delete(route, container); return await Save(container, file);
        }
    }
}
