using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared.JwtTokenAuthentication.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetUserSecurityTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SecurityKey = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserSecurityTokens", x => x.UserId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserSecurityTokens_UserId",
                table: "AspNetUserSecurityTokens",
                column: "UserId")
                .Annotation("SqlServer:Clustered", false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetUserSecurityTokens");
        }
    }
}
