namespace BracketMaker.Models;

public class GameAnswer
{
    public string GameID { get; set; }
    public int QuestionID { get; set; }
    public string UserName { get; set; }
    public Answers Answer { get; set; } = Answers.None;
}