using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRefreshTokenCompositeKeyToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRefreshTokens",
                table: "AspNetUserRefreshTokens");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "AspNetUserRefreshTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRefreshTokens",
                table: "AspNetUserRefreshTokens",
                columns: new[] { "UserId", "Token" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRefreshTokens",
                table: "AspNetUserRefreshTokens");

            migrationBuilder.AlterColumn<string>(
                name: "Token",
                table: "AspNetUserRefreshTokens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRefreshTokens",
                table: "AspNetUserRefreshTokens",
                column: "UserId");
        }
    }
}
