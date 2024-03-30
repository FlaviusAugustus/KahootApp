using KahootBackend.ClientMethods;
using KahootBackend.Models;
using KahootBackend.Services;
using Microsoft.AspNetCore.SignalR;
namespace KahootBackend.Hubs;

public class GameHub(IGameService gameService) : Hub<IGameTypedHub>
{
    private ConnectionStore<string> _connectionStore = new();
    
    public override async Task OnConnectedAsync()
    {
        var name = Context.User?.Identity?.Name;
        if (name is null)
        {
            await base.OnConnectedAsync();
            return;
        }

        foreach (var group in _connectionStore.GetGroups(name))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }
        
        _connectionStore.AddConnection(name, Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var name = Context.User?.Identity?.Name;
        if (name is null)
        {
            await base.OnDisconnectedAsync(exception);
            return;
        }
        
        _connectionStore.RemoveConnection(name, Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }
}