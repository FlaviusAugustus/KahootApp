using BracketMaker.Models;

namespace BracketMaker.Services;

public interface IGameService
{
    // groupID => Host ConnectionID
    public Dictionary<string, GameInfo> GameHosts { get; init; } 
    
    public string GenerateGroupID();
}