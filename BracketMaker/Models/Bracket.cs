namespace BracketMaker.Models;

public class Bracket : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Name { get; set; }
    public ICollection<Item> Items { get; set; }
}