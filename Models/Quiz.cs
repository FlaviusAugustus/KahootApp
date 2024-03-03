namespace BracketMaker.Models;

public class Quiz : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public IList<Question> Questions { get; set; } = new List<Question>();
    public IList<Tag> Tags { get; set; } = new List<Tag>();
    
    public Guid OwnerId { get; set; }
}