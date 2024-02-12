using BracketMaker.AppConfigurationExtensions;
using BracketMaker.Constants;
using BracketMaker.Context.Configuration;
using BracketMaker.ItemContext.Configuration;
using BracketMaker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BracketMaker.Context;

public class ItemContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Quiz> Quizzes { get; set; }
    
    public ItemContext(DbContextOptions<ItemContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=data.db");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuizConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}