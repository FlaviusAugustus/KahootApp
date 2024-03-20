using KahootBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KahootBackend.ItemContext.Configuration;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder
            .HasOne(i => i.Quiz)
            .WithMany(b => b.Questions)
            .HasForeignKey(i => i.QuizId);

        builder
            .HasMany(q => q.Choices)
            .WithOne(c => c.Question)
            .HasForeignKey(c => c.QuestionID);

        builder
            .Property(q => q.ImageUrl)
            .IsRequired(false);

        builder
            .Property(i => i.Value)
            .HasMaxLength(32);
    }
}