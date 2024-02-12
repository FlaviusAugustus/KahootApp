using BracketMaker.Models;
using LanguageExt;
using LanguageExt.Common;

namespace QuizApi.Services.UserService;

public interface IUserService
{
    Task<Result<User>> RegisterAsync(RegisterModel registerModel);
    Task<Result<AuthModel>> GetTokenAsync(TokenRequestModel requestModel);
    Task<Result<Unit>> AddToRoleAsync(ManageRoleModel roleModel);
    Task<Result<Unit>> RemoveRoleAsync(ManageRoleModel roleModel);
    Task<IList<string>> GetUserRoles(string userName);
}