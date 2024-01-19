using BracketMaker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BracketMaker.ItemContext.Configuration;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder
            .HasOne(i => i.Bracket)
            .WithMany(b => b.Items)
            .HasForeignKey(i => i.BracketId);

        builder
            .Property(i => i.Name)
            .HasMaxLength(32);
    }
}