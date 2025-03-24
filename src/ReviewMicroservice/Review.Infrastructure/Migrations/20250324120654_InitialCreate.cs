using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Review.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReviewAuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Action = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TableId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewAuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewReportLookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descriptions = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewReportLookups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Star = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsReviewed = table.Column<bool>(type: "bit", nullable: false),
                    ReviewedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReviewedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReviewReason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Reviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewId = table.Column<int>(type: "int", nullable: false),
                    ReviewReportLookUpId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    isHelpful = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewDetail_ReviewReportLookup",
                        column: x => x.ReviewReportLookUpId,
                        principalTable: "ReviewReportLookup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReviewDetail_Reviews",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewAuditLogs_TableId",
                table: "ReviewAuditLogs",
                column: "TableId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewAuditLogs_TableName",
                table: "ReviewAuditLogs",
                column: "TableName")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewAuditLogs_UserId",
                table: "ReviewAuditLogs",
                column: "UserId")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewDetail_ReportTypeId",
                table: "ReviewDetails",
                column: "ReviewReportLookUpId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewDetail_ReviewId",
                table: "ReviewDetails",
                column: "ReviewId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewAuditLogs");

            migrationBuilder.DropTable(
                name: "ReviewDetails");

            migrationBuilder.DropTable(
                name: "ReviewReportLookup");

            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
