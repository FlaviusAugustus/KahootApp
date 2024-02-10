using BracketMaker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BracketMaker.ItemContext.Configuration;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder
            .HasOne(i => i.Quiz)
            .WithMany(b => b.Questions)
            .HasForeignKey(i => i.QuizId);

        builder
            .Property(i => i.Name)
            .HasMaxLength(32);
    }
}