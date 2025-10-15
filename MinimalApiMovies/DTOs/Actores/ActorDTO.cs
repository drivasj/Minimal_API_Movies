namespace MinimalApiMovies.DTOs.Actores
{
    public class ActorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime DatebBirth { get; set; }
        public string? Foto { get; set; }
    }
}
