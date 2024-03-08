namespace KahootBackend.Models;

public class QuestionDto
{
    public string Value { get; set; } = string.Empty;
    public int Time { get; set; } = int.MaxValue;
    public string? ImageUrl { get; set; } = string.Empty;
    public ICollection<ChoiceDto> Choices { get; set; } = new List<ChoiceDto>();
}