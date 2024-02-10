using System.Text.Json.Serialization;

namespace BracketMaker.Models;

public class Item : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Name { get; set; }
    
    [JsonIgnore]
    public Guid BracketId { get; set; }
    [JsonIgnore]
    public Quiz Quiz { get; set; }
}