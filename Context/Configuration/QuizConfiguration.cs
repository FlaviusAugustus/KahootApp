using KahootBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KahootBackend.Context.Configuration;

public class QuizConfiguration :IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder
            .HasMany(i => i.Questions)
            .WithOne(i => i.Quiz)
            .HasForeignKey(i => i.QuizId);

        builder
            .Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(32);

        builder
            .HasMany(q => q.Tags)
            .WithMany(t => t.Quizzes)
            .UsingEntity<QuizTag>();
    }
}