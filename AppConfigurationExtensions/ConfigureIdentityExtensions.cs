using KahootBackend.Models;
using Microsoft.AspNetCore.Identity;

namespace KahootBackend.AppConfigurationExtensions;

public static class ConfigureIdentityExtensions
{
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole<Guid>>().AddEntityFrameworkStores<Context.ItemContext>();
        services.Configure<IdentityOptions>(opts =>
        {
            opts.Password.RequireDigit = false;
            opts.Password.RequiredLength = 0;
            opts.Password.RequireLowercase = false;
            opts.Password.RequireUppercase = false;
            opts.Password.RequiredUniqueChars = 0;
            opts.Password.RequireNonAlphanumeric = false;
        });
    }
}