namespace KahootBackend.Models.Mappings;

public static class QuizDtoToQuiz
{
    public static Quiz ToQuiz(this QuizDto quizDto)
    {
        var quiz = new Quiz
        {
            Title = quizDto.Title,
            Description = quizDto.Description,
            ImageUrl = quizDto.ImageUrl,
            CreatedAt = DateTime.Now
        };
        
        quiz.Questions = quizDto.Questions
            .Select(q => q.ToQuestion(quiz))
            .ToList();
        
        return quiz;
    }
}