using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KahootBackend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeQuizModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Quizzes",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Question",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "Answer",
                table: "Question",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "AllAnswers",
                table: "Question",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Quizzes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Quizzes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Choice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Answer = table.Column<string>(type: "TEXT", nullable: false),
                    Correct = table.Column<bool>(type: "INTEGER", nullable: false),
                    QuestionID = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Choice_Question_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("62e661b4-0ce0-4e83-a8f6-1925b3acae87"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f60e43cf-88c9-4d08-8ad6-a9f7f4b763bf", "AQAAAAIAAYagAAAAEMpG0ybHApH62YNWAkDJOyuHus9tWFuZ1860bGQHeA+cCfxcN5GgtSRzXjuJdtqzCQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_Choice_QuestionID",
                table: "Choice",
                column: "QuestionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Choice");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Quizzes");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Quizzes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Question",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Question",
                newName: "Answer");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Question",
                newName: "AllAnswers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("62e661b4-0ce0-4e83-a8f6-1925b3acae87"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8f8fb6f1-31ff-4c40-9034-464a403abb32", "AQAAAAIAAYagAAAAEIN0Ck7MJaD6lgzbxj9xm+Pmhwk31zzpxtwJ8ZVOvYUXycfwot6GBc5ItmnUSNQ6Gw==" });
        }
    }
}
