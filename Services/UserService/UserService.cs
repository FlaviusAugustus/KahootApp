using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BracketMaker.Models;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using BracketMaker.Constants;
using BracketMaker.Services.DateTimeProvider;
using BracketMaker.Settings;
using QuizApi.Services.UserService;

namespace BracketMaker.Services.UserService;

public class UserService(UserManager<User> userManager,
        RoleManager<IdentityRole<Guid>> roleManager, IOptions<JWT> jwt,
        IDateTimeProvider dateTimeProvider)
    : IUserService
{
    private readonly RoleManager<IdentityRole<Guid>> _roleManager = roleManager;
    private readonly JWT _jwtConfig = jwt.Value;

    public async Task<IList<string>> GetUserRoles(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        return user is not null ?
            await userManager.GetRolesAsync(user) : 
            Array.Empty<string>();
    }

    public async Task<Result<Unit>> RemoveRoleAsync(ManageRoleModel roleModel)
    {
        var user = await userManager.FindByNameAsync(roleModel.UserName);
        return await ManageRole(user, roleModel, RemoveUserFromRole);
    }

    public async Task<Result<Unit>> AddToRoleAsync(ManageRoleModel roleModel)
    {
        var user = await userManager.FindByNameAsync(roleModel.UserName);
        return await ManageRole(user, roleModel, AddUserToRole);
    }

    private static async Task<Result<Unit>> ManageRole(User? user, ManageRoleModel roleModel, Func<User, Role, Task> roleManager)
    {
        if (user is null)
        {
            var error = new ArgumentException($"User {roleModel.UserName} doesn't exist");
            return new Result<Unit>(error);
        }
        
        if (Enum.TryParse<Role>(roleModel.Role, out var role))
        {
            await roleManager(user, role);
            return new Result<Unit>();
        }
        var invalidRoleException = new ArgumentException($"No existing role: {roleModel.Role}");
        return new Result<Unit>(invalidRoleException);
    }

    private async Task RemoveUserFromRole(User user, Role role)
    {
        if (await userManager.IsInRoleAsync(user, role.ToString()))
        {
            await userManager.RemoveFromRoleAsync(user, role.ToString());
        }
    }

    private async Task AddUserToRole(User user, Role role)
    {
        if (!await userManager.IsInRoleAsync(user, role.ToString()))
        {
            await userManager.AddToRoleAsync(user, role.ToString());
        }
    }

    public async Task<Result<AuthModel>> GetTokenAsync(TokenRequestModel requestModel)
    {
        var user = await userManager.FindByEmailAsync(requestModel.Email);
        if (user is null)
        {
            var noAccountError = new ArgumentException($"No accounts with Email: {requestModel.Email}");
            return new Result<AuthModel>(noAccountError);
        }

        if (await userManager.CheckPasswordAsync(user, requestModel.Password))
        {
            var token = await CreateTokenForUser(user);
            return new Result<AuthModel>(token);
        }
        
        var invalidCredentialsError = new ArgumentException($"Invalid Credentials for user {user.UserName}");
        return new Result<AuthModel>(invalidCredentialsError);
    }

    private async Task<AuthModel> CreateTokenForUser(User user)
    {
        var roles = await userManager.GetRolesAsync(user);
        return new AuthModel
        {
            IsAuthenticated = true,
            Token = new JwtSecurityTokenHandler().WriteToken(await CreateJwtToken(user)),
            Email = user.Email,
            UserName = user.UserName,
            Roles = roles.ToList(),
        };
    }

    private async Task<JwtSecurityToken> CreateJwtToken(User user)
    {
        var claims = await CreateClaims(user);
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
        var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);
        var expires = dateTimeProvider.GetCurrentTime().AddMinutes(_jwtConfig.DurationInMinutes);

        return new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials);
    }

    private async Task<IEnumerable<Claim>> CreateClaims(User user)
    {
        var userClaims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(j => new Claim("roles", j));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        
        return claims.Union(userClaims).Union(roleClaims);
    }

    public async Task<Result<User>> RegisterAsync(RegisterModel registerModel)
    {
        var user = new User
        {
            UserName = registerModel.UserName,
            Email = registerModel.Email,
            CreatedAt = dateTimeProvider.GetCurrentTime(),
        };
        if (await IsUserDataTaken(user))
        {
            var credentialsError = new ArgumentException("Invalid Credentials");
            return new Result<User>(credentialsError);
        }
        
        var result = await userManager.CreateAsync(user, registerModel.Password);
        await AddUserToRole(user, Role.User);
        if (result.Succeeded)
        {
            return new Result<User>(user);
        }
        
        var creationError = new ArgumentException($"Couldn't create user {user.UserName}");
        return new Result<User>(creationError);
    }

    private async Task<bool> IsUserDataTaken(User user)
    {
        var sameEmail = await  userManager.FindByEmailAsync(user.Email);
        return sameEmail is not null;
    }
}