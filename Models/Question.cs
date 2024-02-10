namespace BracketMaker.Models;

public class Question : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string Name { get; set; } = string.Empty;
    public IEnumerable<string> AllAnswers { get; set; } = Enumerable.Empty<string>();
    public Answers Answer { get; set; } = 0;
    
    public Guid QuizId { get; set; }
    public Quiz Quiz { get; set; }
}