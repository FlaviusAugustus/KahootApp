using KahootBackend.Models;
using LanguageExt;
using LanguageExt.Common;

namespace KahootBackend.Services;

public interface IGameService
{
    public Dictionary<string, GameInfo> GameHosts { get; init; }
    public void AddGame(string connectionId, string groupID);

    public GameInfo GetGameState(string connectionId);
    
    public void StartGame(string connectionId);
    
    public void RemoveGame(string connectionId);
    public string GetHost(string groupId);
    public string GenerateGroupID();
    public bool ProcessAnswer(GameAnswer answer);
    public (bool Success, string Message) JoinGroup(JoinRequest joinRequest);
}