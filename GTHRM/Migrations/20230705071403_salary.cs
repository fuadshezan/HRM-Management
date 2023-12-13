using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GTHRM.Migrations
{
    /// <inheritdoc />
    public partial class salary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "salaries",
                columns: table => new
                {
                    ComId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    month = table.Column<int>(type: "int", nullable: false),
                    gross = table.Column<double>(type: "float", nullable: false),
                    basic = table.Column<double>(type: "float", nullable: false),
                    hrent = table.Column<double>(type: "float", nullable: false),
                    medical = table.Column<double>(type: "float", nullable: false),
                    absentAmount = table.Column<double>(type: "float", nullable: false),
                    payableAmount = table.Column<double>(type: "float", nullable: false),
                    isPaid = table.Column<bool>(type: "bit", nullable: false),
                    paidAmount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salaries", x => new { x.ComId, x.EmpId, x.year, x.month });
                    table.ForeignKey(
                        name: "FK_salaries_Company_ComId",
                        column: x => x.ComId,
                        principalTable: "Company",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_salaries_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "EmpId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_salaries_EmpId",
                table: "salaries",
                column: "EmpId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "salaries");
        }
    }
}
