using KahootBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KahootBackend.Context.Configuration;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder
            .HasMany(t => t.Quizzes)
            .WithMany(q => q.Tags)
            .UsingEntity<QuizTag>();

        builder
            .Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(32);
    }
}