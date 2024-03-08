using KahootBackend.Repository;
using KahootBackend.Repository.QuizRepository;
using KahootBackend.Services;
using Microsoft.EntityFrameworkCore;

namespace KahootBackend.AppConfigurationExtensions;

public static class ConfigureDatabaseExtensions
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IGameService, GameService>();
        services.AddDbContext<Context.ItemContext>(opts =>
        {
            opts.UseSqlite(config.GetConnectionString("database"));
        });
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); 
        services.AddScoped<IQuizRepository, QuizRepository>(); 
    }
}