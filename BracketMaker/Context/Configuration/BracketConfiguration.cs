using BracketMaker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BracketMaker.ItemContext.Configuration;

public class BracketConfiguration :IEntityTypeConfiguration<Bracket>
{
    public void Configure(EntityTypeBuilder<Bracket> builder)
    {
        builder
            .HasMany(i => i.Items)
            .WithOne(i => i.Bracket)
            .HasForeignKey(i => i.BracketId);

        builder
            .Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(32);
    }
}