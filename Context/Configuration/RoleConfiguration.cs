using KahootBackend.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KahootBackend.Context.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
    {
        var adminRole = new IdentityRole<Guid>
        {
            Id = new Guid("0a5eb0bc-ebc6-427e-a5e4-01b0ea77132f"),
            Name = "Admin",
            NormalizedName = "ADMIN"
        };
        
        var moderatorRole = new IdentityRole<Guid>
        {
            Id = new Guid("dcbdc40a-621a-47ef-95a9-390ec1b1b490"),
            Name = "Moderator",
            NormalizedName = "MODERATOR"
        };
        
        var userRole = new IdentityRole<Guid>
        {
            Id = new Guid("11e32dd8-2e37-41e0-83d5-0c899bd12dd5 "),
            Name = "User",
            NormalizedName = "USER"
        };
        
        builder.HasData(adminRole);
        builder.HasData(moderatorRole);
        builder.HasData(userRole);
    }
}