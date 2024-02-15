namespace BracketMaker.Models;

public class Question : Entity
{
    public string Name { get; set; } = string.Empty;
    public IEnumerable<string> AllAnswers { get; set; } = Enumerable.Empty<string>();
    public Answers Answer { get; set; } = 0;
    
    public Guid QuizId { get; set; }
    public Quiz Quiz { get; set; }
}