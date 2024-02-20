using BracketMaker.Models;
using LanguageExt;
using LanguageExt.Common;

namespace BracketMaker.Services;

public interface IGameService
{
    public Dictionary<string, GameInfo> GameHosts { get; init; }
    public void AddGame(string connectionId);

    public GameInfo GetGameState(string connectionId);
    
    public void StartGame(string connectionId);
    
    public void RemoveGame(string connectionId);
    public string GetHost(string groupId);
    public string GenerateGroupID();
    public bool ProcessAnswer(GameAnswer answer);
    public (bool Success, string Message) JoinGroup(JoinRequest joinRequest);
}