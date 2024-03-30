using Microsoft.AspNetCore.SignalR;

namespace KahootBackend.Services.UserIdProvider;

public class UserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection) =>
        connection.User.Claims
            .SingleOrDefault(c => c.Type == "name")?
            .ToString();
}