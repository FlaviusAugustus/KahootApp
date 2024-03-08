namespace KahootBackend.Models.Mappings;

public static class ChoiceDtoToChoice
{
    public static Choice ToChoice(this ChoiceDto choiceDto) => new Choice
    {
        Answer = choiceDto.Answer,
        Correct = choiceDto.Correct,
        CreatedAt = DateTime.Now
    };
}