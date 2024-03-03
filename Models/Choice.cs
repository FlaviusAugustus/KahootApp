namespace BracketMaker.Models;

public class Choice : Entity
{
    public string? Answer { get; set; } = string.Empty;
    public bool Correct { get; set; }
    
    public Guid QuestionID { get; set; }
    public Question Question { get; set; }
}