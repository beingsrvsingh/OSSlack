using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kathavachak.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTimeSlots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kathavachak_time_slot");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "kathavachak_time_slot",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    schedule_id = table.Column<int>(type: "int", nullable: false),
                    day_of_week = table.Column<int>(type: "int", nullable: false),
                    end_time = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    start_time = table.Column<TimeOnly>(type: "time(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kathavachak_time_slot", x => x.id);
                    table.ForeignKey(
                        name: "FK_kathavachak_time_slot_kathavachak_schedule_schedule_id",
                        column: x => x.schedule_id,
                        principalTable: "kathavachak_schedule",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_time_slots_schedule_id",
                table: "kathavachak_time_slot",
                column: "schedule_id");
        }
    }
}
