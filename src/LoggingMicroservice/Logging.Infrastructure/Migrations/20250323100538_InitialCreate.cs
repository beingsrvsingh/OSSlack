using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logging.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppsLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    IpAddress = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Logged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Level = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Message = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    Exception = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    Callsite = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    Logger = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppsLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    IpAddress = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Logged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Level = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Message = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    Exception = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    Callsite = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    Logger = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppsLog_Id",
                table: "AppsLog",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Log_Id",
                table: "Log",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppsLog");

            migrationBuilder.DropTable(
                name: "Log");
        }
    }
}
