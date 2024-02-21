using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BracketMaker.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_AspNetUsers_OwnerId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_OwnerId",
                table: "Quizzes");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Quizzes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("62e661b4-0ce0-4e83-a8f6-1925b3acae87"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8f8fb6f1-31ff-4c40-9034-464a403abb32", "AQAAAAIAAYagAAAAEIN0Ck7MJaD6lgzbxj9xm+Pmhwk31zzpxtwJ8ZVOvYUXycfwot6GBc5ItmnUSNQ6Gw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_UserId",
                table: "Quizzes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_AspNetUsers_UserId",
                table: "Quizzes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_AspNetUsers_UserId",
                table: "Quizzes");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_UserId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Quizzes");

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
    }
}
