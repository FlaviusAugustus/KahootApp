namespace BracketMaker.Models;

public class GameInfo
{
    public string HostConnectionID { get; set; }
    public bool IsStarted { get; set; } = false;
    public ICollection<PlayerInfo> Players { get; set; } = new List<PlayerInfo>();
    public IList<Question> Questions { get; set; } = new List<Question>();
}