using BracketMaker.Models;
using LanguageExt;
using LanguageExt.Common;

namespace BracketMaker.Services;

public interface IGameService
{
    public Dictionary<string, GameInfo> GameHosts { get; init; } 
    public string GenerateGroupID();
    public bool ProcessAnswer(GameAnswer answer);
    public (bool Success, string Message) JoinGroup(JoinRequest joinRequest);
}