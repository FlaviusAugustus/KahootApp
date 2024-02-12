using BracketMaker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Services.UserService;

namespace BracketMaker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(IUserService userService) : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAsync(RegisterModel registerModel)
    {
        var result = await userService.RegisterAsync(registerModel);
        return result.Match<IActionResult>(
            success => Ok(success),
            fail => BadRequest(fail.Message)
        );
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginAsync(TokenRequestModel requestModel)
    {
        var result = await userService.GetTokenAsync(requestModel);
        return result.Match<IActionResult>(
            success => success is null ? NotFound() : Ok(success),
                fail => BadRequest(fail.Message)
            );
    }

    [HttpGet]
    [Route("get-roles")]
    [Authorize]
    public async Task<IActionResult> GetUserRoles(string userName)
    {
        var roles = await userService.GetUserRoles(userName);
        return Ok(roles);
    }

    [HttpPost]
    [Route("add-role")]
    [Authorize] // TODO: Add policies
    public async Task<IActionResult> AddToRole(ManageRoleModel roleModel)
    {
        var result = await userService.AddToRoleAsync(roleModel);
        return result.Match<IActionResult>(
            success => Ok(success),
            fail => result.IsBottom ? Ok() : BadRequest(fail.Message) // Temporary till i find a better fix
            );
    }

    [HttpPost]
    [Route("remove-role")]
    [Authorize] // TODO: Add policies
    public async Task<IActionResult> RemoveRole(ManageRoleModel roleModel)
    {
        var result = await userService.RemoveRoleAsync(roleModel);
        return result.Match<IActionResult>(
            success => Ok(),
            fail => result.IsBottom ? Ok() : BadRequest(fail.Message) // Temporary till i find a better fix
        );
    }
}