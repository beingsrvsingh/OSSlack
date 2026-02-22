using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kathavachak.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddScheduleExceptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_booked",
                table: "kathavachak_time_slot");

            migrationBuilder.CreateTable(
                name: "schedule_exceptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    kathavachak_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    start_time = table.Column<TimeSpan>(type: "time(6)", nullable: true),
                    end_time = table.Column<TimeSpan>(type: "time(6)", nullable: true),
                    is_blocked = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedule_exceptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_schedule_exceptions_kathavachak_master_kathavachak_id",
                        column: x => x.kathavachak_id,
                        principalTable: "kathavachak_master",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_exceptions_kathavachak_id_date",
                table: "schedule_exceptions",
                columns: new[] { "kathavachak_id", "date" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "schedule_exceptions");

            migrationBuilder.AddColumn<bool>(
                name: "is_booked",
                table: "kathavachak_time_slot",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
