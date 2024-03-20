using KahootBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KahootBackend.Context.Configuration;

public class AdminConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var admin = new User
        {
            Id = new Guid("62e661b4-0ce0-4e83-a8f6-1925b3acae87"),
            CreatedAt = DateTime.UnixEpoch,
            UserName = "Admin",
            NormalizedUserName = "ADMIN",
            Email = "Admin@email.com",
            NormalizedEmail = "ADMIN@EMAIL.COM"
        };
        admin.PasswordHash = new PasswordHasher<User>().HashPassword(admin, "password");

        builder.HasData(admin);
    }
}