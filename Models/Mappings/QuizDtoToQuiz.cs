namespace BracketMaker.Models.Mappings;

public static class QuizDtoToQuiz
{
    public static Quiz ToQuiz(this QuizDto quizDto)
    {
        var quiz = new Quiz
        {
            Name = quizDto.Name
        };
        
        quiz.Questions = quizDto.Questions
            .Select(q => q.ToQuestion(quiz))
            .ToList();
        
        return quiz;
    }
}