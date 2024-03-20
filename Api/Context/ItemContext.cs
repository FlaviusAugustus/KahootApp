﻿using KahootBackend.AppConfigurationExtensions;
using KahootBackend.Constants;
using KahootBackend.ItemContext.Configuration;
using KahootBackend.Context.Configuration;
using KahootBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KahootBackend.Context;

public class ItemContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Quiz> Quizzes { get; set; }

    public ItemContext(DbContextOptions<Context.ItemContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

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