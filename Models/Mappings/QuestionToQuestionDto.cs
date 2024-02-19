namespace BracketMaker.Models.Mappings;

public static class QuestionToQuestionDto
{
    public static QuestionDto ToDto(this Question question) => new QuestionDto
    {
        AllAnswers = question.AllAnswers,
        Answer = question.Answer,
        Name = question.Name
    };

}