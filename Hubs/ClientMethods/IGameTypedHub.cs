using KahootBackend.Models;

namespace KahootBackend.ClientMethods;

public interface IGameTypedHub
{
    Task OnGroupCreated(GroupInfo groupInfo);
    Task PlayerJoined(string userName);
    Task PlayerAnswered(string result);
    Task NextQuestion(Question question);
    
    Task FailedToJoinGame(string message);
    Task GameJoined(string userName);
    Task ReceiveQuestionResult(string result);
}