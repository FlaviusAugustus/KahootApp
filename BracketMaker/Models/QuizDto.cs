namespace BracketMaker.Models;

public class QuizDto
{
    public string Name { get; set; }
    public IList<QuestionDto> Questions { get; set; }
}