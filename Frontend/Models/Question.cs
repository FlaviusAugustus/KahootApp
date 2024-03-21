using Microsoft.VisualBasic;

namespace KahootFrontend.Models;

public class Question : Entity
{
    public string Value { get; set; } = string.Empty;
    public int Time { get; set; } = int.MaxValue;
    public string? ImageUrl { get; set; } = string.Empty;
    public ICollection<Choice> Choices { get; set; } = new List<Choice>();
    
    public Guid QuizId { get; set; }
    public Quiz Quiz { get; set; }
}