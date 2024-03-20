namespace KahootBackend.Models;

public class QuizDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public IList<QuestionDto> Questions { get; set; } = new List<QuestionDto>();
}