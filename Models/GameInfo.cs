namespace BracketMaker.Models;

public class GameInfo
{
    public string HostConnectionID { get; set; }
    public bool IsStarted { get; set; } = false;
    public ICollection<PlayerInfo> Players { get; set; } = new List<PlayerInfo>();
    public Quiz Quiz { get; set; } = new Quiz();
}