using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GTHRM.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    ComId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Basic = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Hrent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Medical = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Others = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.ComId);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DeptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ComId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DeptId);
                    table.ForeignKey(
                        name: "FK_Departments_Company_ComId",
                        column: x => x.ComId,
                        principalTable: "Company",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    DesigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ComId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DesigName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.DesigId);
                    table.ForeignKey(
                        name: "FK_Designations_Company_ComId",
                        column: x => x.ComId,
                        principalTable: "Company",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    ShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ComId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShiftIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftLate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.ShiftId);
                    table.ForeignKey(
                        name: "FK_Shifts_Company_ComId",
                        column: x => x.ComId,
                        principalTable: "Company",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmpId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ComId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmpName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DesigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    Gross = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Basic = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Hrent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Medical = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Others = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    dtJoin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmpId);
                    table.ForeignKey(
                        name: "FK_Employees_Company_ComId",
                        column: x => x.ComId,
                        principalTable: "Company",
                        principalColumn: "ComId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DeptId",
                        column: x => x.DeptId,
                        principalTable: "Departments",
                        principalColumn: "DeptId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_Designations_DesigId",
                        column: x => x.DesigId,
                        principalTable: "Designations",
                        principalColumn: "DesigId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "ShiftId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_CompanyName",
                table: "Company",
                column: "CompanyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ComId_DepartmentName",
                table: "Departments",
                columns: new[] { "ComId", "DepartmentName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Designations_ComId_DesigName",
                table: "Designations",
                columns: new[] { "ComId", "DesigName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ComId_EmpCode",
                table: "Employees",
                columns: new[] { "ComId", "EmpCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DeptId",
                table: "Employees",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DesigId",
                table: "Employees",
                column: "DesigId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ShiftId",
                table: "Employees",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_ComId",
                table: "Shifts",
                column: "ComId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Designations");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
