namespace BracketMaker.Models;

public class Quiz : Entity
{
    public string Name { get; set; } = string.Empty;
    public IList<Question> Questions { get; set; } = new List<Question>();
    public IList<Tag> Tags { get; set; } = new List<Tag>();
    
    public User Owner { get; set; }
    public Guid OwnerId { get; set; }
}