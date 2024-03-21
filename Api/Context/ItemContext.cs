using KahootBackend.AppConfigurationExtensions;
using KahootBackend.Constants;
using KahootBackend.ItemContext.Configuration;
using KahootBackend.Context.Configuration;
using KahootBackend.Models;
using KahootBackend.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KahootBackend.Context;

public class ItemContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Quiz> Quizzes { get; set; }

    private IConfiguration  _config;

    public ItemContext(DbContextOptions<Context.ItemContext> options, IConfiguration config) : base(options)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_config.GetConnectionString("Database"));
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuizConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}