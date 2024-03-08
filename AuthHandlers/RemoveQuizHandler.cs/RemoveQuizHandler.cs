using KahootBackend.Constants;
using KahootBackend.Models;
using Microsoft.AspNetCore.Authorization;

namespace KahootBackend.AuthHandlers.Requirements;

public class RemoveQuizHandler : AuthorizationHandler<CanDeleteQuizRequirement, Quiz>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        CanDeleteQuizRequirement requirement, Quiz quiz)
    {
        if (context.User.IsInRole(nameof(Role.Moderator)) ||
            context.User.IsInRole(nameof(Role.Admin)))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        var userId = context.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

        if (quiz.OwnerId.ToString() != userId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}