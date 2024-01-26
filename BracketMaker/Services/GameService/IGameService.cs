using BracketMaker.Models;
using LanguageExt;
using LanguageExt.Common;

namespace BracketMaker.Services;

public interface IGameService
{
    public Dictionary<string, GameInfo> GameHosts { get; init; } 
    public string GenerateGroupID();
    public (bool Success, string Message) JoinGroup(JoinRequest joinRequest);
}