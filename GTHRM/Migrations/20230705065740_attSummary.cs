using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GTHRM.Migrations
{
    /// <inheritdoc />
    public partial class attSummary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttendanceSummaries",
                columns: table => new
                {
                    ComId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    month = table.Column<int>(type: "int", nullable: false),
                    present = table.Column<int>(type: "int", nullable: false),
                    late = table.Column<int>(type: "int", nullable: false),
                    absent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceSummaries", x => new { x.ComId, x.EmpId, x.year, x.month });
                    table.ForeignKey(
                        name: "FK_AttendanceSummaries_Company_ComId",
                        column: x => x.ComId,
                        principalTable: "Company",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceSummaries_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "EmpId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSummaries_EmpId",
                table: "AttendanceSummaries",
                column: "EmpId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendanceSummaries");
        }
    }
}
