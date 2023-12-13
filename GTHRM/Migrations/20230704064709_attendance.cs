using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GTHRM.Migrations
{
    /// <inheritdoc />
    public partial class attendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    comId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    empId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    dtDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    appStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    inTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    outTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => new { x.comId, x.empId, x.dtDate });
                    table.ForeignKey(
                        name: "FK_Attendances_Company_comId",
                        column: x => x.comId,
                        principalTable: "Company",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Attendances_Employees_empId",
                        column: x => x.empId,
                        principalTable: "Employees",
                        principalColumn: "EmpId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_empId",
                table: "Attendances",
                column: "empId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");
        }
    }
}
