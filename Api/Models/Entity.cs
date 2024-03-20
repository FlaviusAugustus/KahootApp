namespace KahootBackend.Models;

public class Entity : IEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
}