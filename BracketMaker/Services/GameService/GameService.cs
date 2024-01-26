using BracketMaker.Models;
using LanguageExt;
using LanguageExt.Common;

namespace BracketMaker.Services;

public class GameService : IGameService
{
    public Dictionary<string, GameInfo> GameHosts { get; init; } = new();
    
    public string GenerateGroupID()
    {
        var roomId = Guid.NewGuid()
           .ToString("n")
           .Take(8)
           .ToString()!;
        return roomId;
    }

    public bool ProcessAnswer(GameAnswer answer)
    {
        var gameInfo = GameHosts[answer.GameID];
        
        var player = gameInfo.Players.Single(p => p.UserName == answer.UserName);
        var question = gameInfo.Questions[answer.QuestionID];

        if ((question.Answer & answer.Answer) == 0)
        {
            return false;
        }
        
        player.Score++;
        return true;
    }

    public (bool Success, string Message) JoinGroup(JoinRequest joinRequest)
    {
          var gameExists = GameHosts.TryGetValue(joinRequest.GroupID, out var gameInfo);
          if (!gameExists)
              return (false, "Given game already exists");
          if (gameInfo!.IsStarted)
              return (false, "Given game already exists");
          if (gameInfo.Players.Exists(p => p.UserName == joinRequest.UserName))
              return (false, "Given game already exists");
          if (!IsUserNameValid(joinRequest.UserName))
              return (false, "Incorrect user name");
          
          return (true, "");
    }

    private static bool IsUserNameValid(string userName) =>
        !string.IsNullOrEmpty(userName) && userName.All(c => char.IsLetter(c) || char.IsNumber(c));
}