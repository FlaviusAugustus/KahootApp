namespace KahootBackend.Hubs;

public class SignalrUserInfo<T> where T : notnull
{
    public HashSet<T> Connections { get; } = [];
    public HashSet<T> Groups { get; } = [];

    public bool IsEmpty => Groups.Count == 0 && Connections.Count == 0;
}