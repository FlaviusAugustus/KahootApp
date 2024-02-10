namespace BracketMaker.Models.Mappings;

public static class QuestionDtoToQuestion
{
    public static Question ToQuestion(this QuestionDto questionDto, Quiz quiz) => new Question
    {
        AllAnswers = questionDto.AllAnswers,
        Answer = questionDto.Answer,
        Quiz = quiz,
        QuizId = quiz.Id
    };
}