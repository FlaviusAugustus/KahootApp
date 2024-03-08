namespace KahootBackend.Models.Mappings;

public static class QuestionDtoToQuestion
{
    public static Question ToQuestion(this QuestionDto questionDto, Quiz quiz)
    {
        var question = new Question
        { 
            Value = questionDto.Value,
            Time = questionDto.Time,
            ImageUrl = questionDto.ImageUrl,
            Choices = questionDto.Choices.Select(c => c.ToChoice()).ToList()
        };
        return question;
    }
}