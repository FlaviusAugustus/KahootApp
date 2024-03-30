namespace KahootBackend.Hubs;

using CollectionSelector = Func<SignalrUserInfo<string>, HashSet<string>>;

public class ConnectionStore<T> where T : notnull
{

    private readonly Dictionary<T, SignalrUserInfo<string>> _userInfo = new();

    public void AddConnection(T key, string connection) =>
        AddEntity(key, connection, info => info.Connections);

    public void AddGroup(T key, string group) =>
        AddEntity(key, group, info => info.Groups);

    public void RemoveConnection(T key, string connection) =>
        RemoveEntity(key, connection, info => info.Connections);
    
    public void RemoveGroup(T key, string group) =>
        RemoveEntity(key, group, info => info.Groups);

    public IEnumerable<string> GetConnections(T key) =>
        GetEntity(key, info => info.Connections);
    
    public IEnumerable<string> GetGroups(T key) =>
        GetEntity(key, info => info.Groups);
    
    private IEnumerable<string> GetEntity(T key, CollectionSelector selector)
    {
        lock (_userInfo) return _userInfo.TryGetValue(key, out var userInfo) ?
                selector(userInfo) :
                Enumerable.Empty<string>();
    }
    
    private void AddEntity(T key, string entity, CollectionSelector selector)
    {
        lock (_userInfo)
        {
            if (!_userInfo.TryGetValue(key, out var userInfo))
            {
                userInfo = new SignalrUserInfo<string>();
                _userInfo.Add(key, userInfo);
            }

            lock (userInfo)
            {
                var col = selector(userInfo);
                col.Add(entity);
            }
        }
    }

    private void RemoveEntity(T key, string entity, CollectionSelector selector)
    {
        lock (_userInfo)
        {
            if (!_userInfo.TryGetValue(key, out var userInfo))
            {
                return;
            }

            lock (userInfo)
            {
                var col = selector(userInfo);
                col.Remove(entity);
                if (userInfo.IsEmpty)
                {
                    _userInfo.Remove(key);
                }
            }
        }
    }
}