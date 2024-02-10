namespace BracketMaker.Models;

public class Quiz : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public string Name { get; set; } = string.Empty;
    public IList<Question> Questions { get; set; } = new List<Question>();
}