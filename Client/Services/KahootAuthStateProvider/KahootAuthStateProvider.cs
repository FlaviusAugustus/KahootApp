using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace KahootFrontend.Services.KahootAuthStateProvider;

public class KahootAuthStateProvider(ProtectedSessionStorage sessionStorage) :
    AuthenticationStateProvider
{
    private static readonly ClaimsPrincipal Anonymous = new(new ClaimsIdentity());
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {

        ProtectedBrowserStorageResult<string> tokenResult;
        try
        {
            tokenResult = await sessionStorage.GetAsync<string>("token");
        }
        catch (InvalidOperationException)
        {
            return new AuthenticationState(Anonymous);
        }

        if (!tokenResult.Success)
        {
            return new AuthenticationState(Anonymous);
        }

        var tokenParsed = new JwtSecurityTokenHandler()
            .ReadToken(tokenResult.Value) as JwtSecurityToken;

        return new AuthenticationState(
            new ClaimsPrincipal(
                new ClaimsIdentity(tokenParsed?.Claims, "jwt")));
    }

    public void AuthenticateUser(string token)
    {
        var tokenParsed = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(
            new ClaimsPrincipal(
                new ClaimsIdentity(tokenParsed?.Claims)))));
    }
}