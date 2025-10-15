namespace MinimalApiMovies.DTOs.Actores
{
    public class CreateActorDTO
    {
        public string Name { get; set; } = null!;
        public DateTime DatebBirth { get; set; }
        public IFormFile? Foto { get; set; }
    }
}
