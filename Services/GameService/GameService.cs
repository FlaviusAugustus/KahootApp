using BracketMaker.Models;
using LanguageExt;
using LanguageExt.Common;

namespace BracketMaker.Services;

public class GameService : IGameService
{
    public Dictionary<string, GameInfo> GameHosts { get; init; } = new();
    private object _locker = new();

    public void AddGame(string connectionId)
    {
        lock (_locker)
        {
            GameHosts.Remove(connectionId);
            GameHosts.Add(connectionId, new GameInfo {HostConnectionID = connectionId});
        }
    }

    public GameInfo GetGameState(string connectionId)
    {
        var gameState = GameHosts[connectionId];
        return new GameInfo
        {
            HostConnectionID = gameState.HostConnectionID,
            IsStarted = gameState.IsStarted,
            Players = new List<PlayerInfo>(gameState.Players),
            Quiz = new Quiz
            {

            }
        };
    }

    public void StartGame(string connectionId)
    {
        lock (_locker)
            GameHosts[connectionId].IsStarted = true;
    }

    public void RemoveGame(string groupId)
    {
        lock (_locker)
        {
            GameHosts.Remove(groupId);
        }
    }

    public string GetHost(string groupId) =>
        GameHosts[groupId].HostConnectionID;
    
    public string GenerateGroupID() =>
        Guid.NewGuid()
           .ToString("n")
           .Take(8)
           .ToString()!;

    public bool ProcessAnswer(GameAnswer answer)
    {
        var gameInfo = GameHosts[answer.GameID];
        
        var player = gameInfo.Players.Single(p => p.UserName == answer.UserName);
        var question = gameInfo.Quiz.Questions[answer.QuestionID];

        player.Score++;
        return true;
    }

    public (bool Success, string Message) JoinGroup(JoinRequest joinRequest)
    {
        var gameExists = GameHosts.TryGetValue(joinRequest.GroupID, out var gameInfo);
        if (!gameExists)
          return (false, "Given game does not exists");
        if (gameInfo!.IsStarted)
          return (false, "Given game already exists");
        if (gameInfo.Players.Exists(p => p.UserName == joinRequest.UserName))
          return (false, "Username taken");
        if (!IsUserNameValid(joinRequest.UserName))
          return (false, "Incorrect user name");

        gameInfo.Players.Add(new PlayerInfo
        {
            UserName = joinRequest.UserName
        });
        
        return (true, "");
    }

    private static bool IsUserNameValid(string userName) =>
        !string.IsNullOrEmpty(userName) && userName.All(c => char.IsLetter(c) || char.IsNumber(c));
}