namespace BracketMaker.Models;

public class Tag : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string Name { get; set; } = string.Empty;
    public IList<Quiz> Quizzes { get; set; } = new List<Quiz>();
}