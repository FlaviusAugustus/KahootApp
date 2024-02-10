namespace BracketMaker.Models;

public class QuestionDto
{
    public string Name { get; set; } = string.Empty;
    public IEnumerable<string> AllAnswers { get; set; } = Enumerable.Empty<string>();
    public Answers Answer { get; set; } = 0;
}