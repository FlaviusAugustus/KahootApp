using BracketMaker.AuthHandlers.Requirements;
using BracketMaker.Constants;

namespace BracketMaker.AppConfigurationExtensions;

public static class ConfigureAuthorizationPoliciesExtensions
{
    public static void ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(nameof(Policy.CanHostAGame),
                policy => policy.RequireRole("User", "Moderator", "Admin"));
            options.AddPolicy(nameof(Policy.CanManageRoles), 
                policy => policy.RequireRole("Admin"));
            options.AddPolicy(nameof(Policy.CanSeeUserRoles),
                policy => policy.RequireRole("Moderator", "Admin"));
            options.AddPolicy(nameof(Policy.CanManageOwnQuizzes),
                policy => policy.Requirements.Add(new CanDeleteQuizRequirement()));
            options.AddPolicy(nameof(Policy.CanManageQuizzesGlobally),
                policy => policy.RequireRole("Moderator", "Admin"));
        });
    }
}