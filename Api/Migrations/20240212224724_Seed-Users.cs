using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KahootBackend.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0a5eb0bc-ebc6-427e-a5e4-01b0ea77132f"), null, "Admin", "ADMIN" },
                    { new Guid("11e32dd8-2e37-41e0-83d5-0c899bd12dd5"), null, "User", "USER" },
                    { new Guid("dcbdc40a-621a-47ef-95a9-390ec1b1b490"), null, "Moderator", "MODERATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("62e661b4-0ce0-4e83-a8f6-1925b3acae87"), 0, "c5cf85ca-532a-4887-92c6-e393a52279c6", new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Admin@email.com", false, false, null, "ADMIN@EMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEIFQtfxE89B8u4PVF7Jtqbjah+dd70C4ue316wCyX1orG4gDNQnNtfMvdoQfr+CEYA==", null, false, null, false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("0a5eb0bc-ebc6-427e-a5e4-01b0ea77132f"), new Guid("62e661b4-0ce0-4e83-a8f6-1925b3acae87") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("11e32dd8-2e37-41e0-83d5-0c899bd12dd5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dcbdc40a-621a-47ef-95a9-390ec1b1b490"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("0a5eb0bc-ebc6-427e-a5e4-01b0ea77132f"), new Guid("62e661b4-0ce0-4e83-a8f6-1925b3acae87") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0a5eb0bc-ebc6-427e-a5e4-01b0ea77132f"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("62e661b4-0ce0-4e83-a8f6-1925b3acae87"));
        }
    }
}
