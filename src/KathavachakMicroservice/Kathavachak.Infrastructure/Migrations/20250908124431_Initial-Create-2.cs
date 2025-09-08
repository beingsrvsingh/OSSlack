using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kathavachak.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_languages_language_code",
                table: "languages");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "languages");

            migrationBuilder.AddColumn<string>(
                name: "language_name",
                table: "languages",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_languages_language_name",
                table: "languages",
                column: "language_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_languages_language_name",
                table: "languages");

            migrationBuilder.DropColumn(
                name: "language_name",
                table: "languages");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "languages",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_languages_language_code",
                table: "languages",
                column: "language_code",
                unique: true);
        }
    }
}
