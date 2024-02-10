using BracketMaker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BracketMaker.Context.Configuration;

public class QuizConfiguration :IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder
            .HasMany(i => i.Questions)
            .WithOne(i => i.Quiz)
            .HasForeignKey(i => i.QuizId);

        builder
            .Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(32);
    }
}