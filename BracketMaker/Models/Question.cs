namespace BracketMaker.Models;

public class Question
{
    public string Name { get; set; }
    public IEnumerable<string> AllAnswers { get; set; }
    public Answers Answer { get; set; }
}