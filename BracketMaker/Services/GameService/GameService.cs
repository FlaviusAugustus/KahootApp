using BracketMaker.Models;

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
}