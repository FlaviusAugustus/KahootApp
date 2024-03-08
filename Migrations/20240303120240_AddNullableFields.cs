using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KahootBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Question",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                table: "Choice",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("62e661b4-0ce0-4e83-a8f6-1925b3acae87"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f286af4f-05a6-4559-a2ca-2ef5cf3bc69c", "AQAAAAIAAYagAAAAEL3k5H1S3gYxkk5v7xr4UzT43qYvcpxUlWYbB62grf5bTH9G+6onO2xXYhMsbc/dZg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Question",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                table: "Choice",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("62e661b4-0ce0-4e83-a8f6-1925b3acae87"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f60e43cf-88c9-4d08-8ad6-a9f7f4b763bf", "AQAAAAIAAYagAAAAEMpG0ybHApH62YNWAkDJOyuHus9tWFuZ1860bGQHeA+cCfxcN5GgtSRzXjuJdtqzCQ==" });
        }
    }
}
