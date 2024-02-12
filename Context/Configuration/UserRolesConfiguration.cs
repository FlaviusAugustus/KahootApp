using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BracketMaker.Context.Configuration;

public class UserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        var adminId = new Guid("62e661b4-0ce0-4e83-a8f6-1925b3acae87");
        var roleId = new Guid("0a5eb0bc-ebc6-427e-a5e4-01b0ea77132f");

        var adminRole = new IdentityUserRole<Guid>
        {
            UserId = adminId,
            RoleId = roleId
        };
        
        builder.HasData(adminRole);
    }
}