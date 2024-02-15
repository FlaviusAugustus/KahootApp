using System.Text.Json.Serialization;

namespace BracketMaker.Models;

public class Item : Entity
{
    public string Name { get; set; }
    
    public Guid BracketId { get; set; }
    public Quiz Quiz { get; set; }
}