using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kathavachak.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    thumbnail_url = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(50)", maxLength: 50, nullable: false),
                    rating_snap = table.Column<int>(type: "int", nullable: false),
                    reviews_snap = table.Column<int>(type: "int", nullable: false),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    sub_category_id = table.Column<int>(type: "int", nullable: false),
                    category_name_snapshot = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sub_category_name_snapshot = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    currency = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_trending = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    is_featured = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kathavachak_master", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kathavachak_expertise",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    kathavachak_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    mrp = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    stock_quantity = table.Column<int>(type: "int", nullable: true),
                    duration_minute = table.Column<int>(type: "int", nullable: false),
                    booking_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_default = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kathavachak_expertise", x => x.id);
                    table.ForeignKey(
                        name: "FK_kathavachak_expertise_kathavachak_master_kathavachak_id",
                        column: x => x.kathavachak_id,
                        principalTable: "kathavachak_master",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kathavachak_image",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    kathavachak_id = table.Column<int>(type: "int", nullable: false),
                    image_url = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    media_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    alt_text = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sort_order = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kathavachak_image", x => x.id);
                    table.ForeignKey(
                        name: "FK_kathavachak_image_kathavachak_master_kathavachak_id",
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
                    language_id = table.Column<int>(type: "int", nullable: false),
                    language_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                name: "kathavachak_addon",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    kathavachak_id = table.Column<int>(type: "int", nullable: true),
                    kathavachak_expertise_id = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    currency = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, defaultValue: "INR")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    display_order = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_kathavachak_addon_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_kathavachak_addon_kathavachak_variant_id",
                        column: x => x.kathavachak_expertise_id,
                        principalTable: "kathavachak_expertise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_kathavachak_addons_kathavachak_id",
                        column: x => x.kathavachak_id,
                        principalTable: "kathavachak_master",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kathavachak_attribute_value",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    catalog_attribute_id = table.Column<int>(type: "int", nullable: false),
                    catalog_attribute_value_id = table.Column<int>(type: "int", nullable: false),
                    value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    attribute_key = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    attribute_label = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    attribute_data_type_id = table.Column<int>(type: "int", nullable: true),
                    catalog_attribute_group_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    kathavachak_id = table.Column<int>(type: "int", nullable: true),
                    expertise_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kathavachak_attribute_value", x => x.id);
                    table.ForeignKey(
                        name: "FK_kathavachak_attribute_value_kathavachak_master_kathavachak_id",
                        column: x => x.kathavachak_id,
                        principalTable: "kathavachak_master",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_kathavachak_attribute_value_expertise_id",
                        column: x => x.expertise_id,
                        principalTable: "kathavachak_expertise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kathavachak_expertise_image",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    kathavachak_expertise_id = table.Column<int>(type: "int", nullable: false),
                    image_url = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    media_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    alt_text = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sort_order = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kathavachak_expertise_image", x => x.id);
                    table.ForeignKey(
                        name: "FK_kathavachak_expertise_image_kathavachak_expertise_kathavacha~",
                        column: x => x.kathavachak_expertise_id,
                        principalTable: "kathavachak_expertise",
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
                    schedule_id = table.Column<int>(type: "int", nullable: false),
                    start_time = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    end_time = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    day_of_week = table.Column<int>(type: "int", nullable: false),
                    is_booked = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
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
                name: "IX_kathavachak_addon_kathavachak_expertise_id",
                table: "kathavachak_addon",
                column: "kathavachak_expertise_id");

            migrationBuilder.CreateIndex(
                name: "IX_kathavachak_addon_kathavachak_id",
                table: "kathavachak_addon",
                column: "kathavachak_id");

            migrationBuilder.CreateIndex(
                name: "IX_kathavachak_attribute_value_kathavachak_id",
                table: "kathavachak_attribute_value",
                column: "kathavachak_id");

            migrationBuilder.CreateIndex(
                name: "ix_kathavachak_attribute_values_kathavachak_catalogattribute",
                table: "kathavachak_attribute_value",
                columns: new[] { "expertise_id", "catalog_attribute_id" });

            migrationBuilder.CreateIndex(
                name: "IX_kathavachak_expertise_kathavachak_id",
                table: "kathavachak_expertise",
                column: "kathavachak_id");

            migrationBuilder.CreateIndex(
                name: "IX_kathavachak_expertise_image_kathavachak_expertise_id",
                table: "kathavachak_expertise_image",
                column: "kathavachak_expertise_id");

            migrationBuilder.CreateIndex(
                name: "IX_kathavachak_image_kathavachak_id",
                table: "kathavachak_image",
                column: "kathavachak_id");

            migrationBuilder.CreateIndex(
                name: "IX_kathavachak_language_kathavachak_id",
                table: "kathavachak_language",
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
                name: "ix_time_slots_schedule_id",
                table: "kathavachak_time_slot",
                column: "schedule_id");

            migrationBuilder.CreateIndex(
                name: "IX_kathavachak_topic_kathavachak_id",
                table: "kathavachak_topic",
                column: "kathavachak_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kathavachak_addon");

            migrationBuilder.DropTable(
                name: "kathavachak_attribute_value");

            migrationBuilder.DropTable(
                name: "kathavachak_expertise_image");

            migrationBuilder.DropTable(
                name: "kathavachak_image");

            migrationBuilder.DropTable(
                name: "kathavachak_language");

            migrationBuilder.DropTable(
                name: "kathavachak_session_mode");

            migrationBuilder.DropTable(
                name: "kathavachak_time_slot");

            migrationBuilder.DropTable(
                name: "kathavachak_topic");

            migrationBuilder.DropTable(
                name: "kathavachak_expertise");

            migrationBuilder.DropTable(
                name: "kathavachak_schedule");

            migrationBuilder.DropTable(
                name: "kathavachak_master");
        }
    }
}
