using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kathavachak.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kathavachak_master",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    profile_picture_url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    average_rating = table.Column<decimal>(type: "decimal(3,2)", nullable: false, defaultValue: 0m),
                    total_ratings = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kathavachak_master", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    language_code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    display_order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_languages", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kathavachak_experties",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    kathavachak_id = table.Column<int>(type: "int", nullable: false),
                    cat_id = table.Column<int>(type: "int", nullable: false),
                    yrs_of_exp = table.Column<int>(type: "int", nullable: false),
                    proficiency_level = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    subcat_id = table.Column<int>(type: "int", nullable: false),
                    subcat_snap = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cat_snap = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kathavachak_experties", x => x.id);
                    table.ForeignKey(
                        name: "FK_kathavachak_experties_kathavachak_master_kathavachak_id",
                        column: x => x.kathavachak_id,
                        principalTable: "kathavachak_master",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kathavachak_media",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    kathavachak_id = table.Column<int>(type: "int", nullable: false),
                    url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    media_type = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kathavachak_media", x => x.id);
                    table.ForeignKey(
                        name: "FK_kathavachak_media_kathavachak_master_kathavachak_id",
                        column: x => x.kathavachak_id,
                        principalTable: "kathavachak_master",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kathavachak_schedule",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    kathavachak_id = table.Column<int>(type: "int", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_available = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    is_recurring = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kathavachak_schedule", x => x.id);
                    table.ForeignKey(
                        name: "FK_kathavachak_schedule_kathavachak_master_kathavachak_id",
                        column: x => x.kathavachak_id,
                        principalTable: "kathavachak_master",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kathavachak_session_mode",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    kathavachak_id = table.Column<int>(type: "int", nullable: false),
                    mode_name = table.Column<int>(type: "int", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kathavachak_session_mode", x => x.id);
                    table.ForeignKey(
                        name: "FK_kathavachak_session_mode_kathavachak_master_kathavachak_id",
                        column: x => x.kathavachak_id,
                        principalTable: "kathavachak_master",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kathavachak_time_slot",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    kathavachak_id = table.Column<int>(type: "int", nullable: false),
                    start_time = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    end_time = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    day_of_week = table.Column<int>(type: "int", nullable: false),
                    is_booked = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kathavachak_time_slot", x => x.id);
                    table.ForeignKey(
                        name: "FK_kathavachak_time_slot_kathavachak_master_kathavachak_id",
                        column: x => x.kathavachak_id,
                        principalTable: "kathavachak_master",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kathavachak_topic",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    kathavachak_id = table.Column<int>(type: "int", nullable: false),
                    topic_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kathavachak_topic", x => x.id);
                    table.ForeignKey(
                        name: "FK_kathavachak_topic_kathavachak_master_kathavachak_id",
                        column: x => x.kathavachak_id,
                        principalTable: "kathavachak_master",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kathavachak_language",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    kathavachak_id = table.Column<int>(type: "int", nullable: false),
                    language_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kathavachak_language", x => x.id);
                    table.ForeignKey(
                        name: "fk_kathavachak_language_kathavachak_id",
                        column: x => x.kathavachak_id,
                        principalTable: "kathavachak_master",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_kathavachak_language_language_id",
                        column: x => x.language_id,
                        principalTable: "languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_kathavachak_experties_kathavachak_id",
                table: "kathavachak_experties",
                column: "kathavachak_id");

            migrationBuilder.CreateIndex(
                name: "IX_kathavachak_language_kathavachak_id",
                table: "kathavachak_language",
                column: "kathavachak_id");

            migrationBuilder.CreateIndex(
                name: "IX_kathavachak_language_language_id",
                table: "kathavachak_language",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "IX_kathavachak_media_kathavachak_id",
                table: "kathavachak_media",
                column: "kathavachak_id");

            migrationBuilder.CreateIndex(
                name: "IX_kathavachak_schedule_kathavachak_id",
                table: "kathavachak_schedule",
                column: "kathavachak_id");

            migrationBuilder.CreateIndex(
                name: "IX_kathavachak_session_mode_kathavachak_id",
                table: "kathavachak_session_mode",
                column: "kathavachak_id");

            migrationBuilder.CreateIndex(
                name: "IX_kathavachak_time_slot_kathavachak_id",
                table: "kathavachak_time_slot",
                column: "kathavachak_id");

            migrationBuilder.CreateIndex(
                name: "IX_kathavachak_topic_kathavachak_id",
                table: "kathavachak_topic",
                column: "kathavachak_id");

            migrationBuilder.CreateIndex(
                name: "IX_languages_language_code",
                table: "languages",
                column: "language_code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kathavachak_experties");

            migrationBuilder.DropTable(
                name: "kathavachak_language");

            migrationBuilder.DropTable(
                name: "kathavachak_media");

            migrationBuilder.DropTable(
                name: "kathavachak_schedule");

            migrationBuilder.DropTable(
                name: "kathavachak_session_mode");

            migrationBuilder.DropTable(
                name: "kathavachak_time_slot");

            migrationBuilder.DropTable(
                name: "kathavachak_topic");

            migrationBuilder.DropTable(
                name: "languages");

            migrationBuilder.DropTable(
                name: "kathavachak_master");
        }
    }
}
