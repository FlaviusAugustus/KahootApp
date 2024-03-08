namespace KahootBackend.Models;

public class Tag : Entity
{
    public string Name { get; set; } = string.Empty;
    public IList<Quiz> Quizzes { get; set; } = new List<Quiz>();
}