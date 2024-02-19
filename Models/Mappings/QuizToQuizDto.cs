namespace BracketMaker.Models.Mappings;

public static class QuizToQuizDto
{
    public static QuizDto ToDto(this Quiz quiz) => new QuizDto
    {
        Name = quiz.Name,
        Questions = quiz.Questions
            .Select(q => q.ToDto())
            .ToList(),
    };
}