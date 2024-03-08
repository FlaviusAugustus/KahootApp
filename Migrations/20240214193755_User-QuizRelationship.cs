using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KahootBackend.Migrations
{
    /// <inheritdoc />
    public partial class UserQuizRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Quizzes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("62e661b4-0ce0-4e83-a8f6-1925b3acae87"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7235feb5-a925-4f91-a548-c5f7fe0b4640", "AQAAAAIAAYagAAAAEBnEbmmEzdV0gpY4Z5Pd7JUj8SyyoN7ZhdgdvIADSf8PTUBQlspHwEdRrqVlYEC35A==" });

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_OwnerId",
                table: "Quizzes",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_AspNetUsers_OwnerId",
                table: "Quizzes",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_AspNetUsers_OwnerId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_OwnerId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Quizzes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("62e661b4-0ce0-4e83-a8f6-1925b3acae87"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c5cf85ca-532a-4887-92c6-e393a52279c6", "AQAAAAIAAYagAAAAEIFQtfxE89B8u4PVF7Jtqbjah+dd70C4ue316wCyX1orG4gDNQnNtfMvdoQfr+CEYA==" });
        }
    }
}
