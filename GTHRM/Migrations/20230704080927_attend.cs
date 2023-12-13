using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GTHRM.Migrations
{
    /// <inheritdoc />
    public partial class attend : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Company_comId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Employees_empId",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "empId",
                table: "Attendances",
                newName: "EmpId");

            migrationBuilder.RenameColumn(
                name: "comId",
                table: "Attendances",
                newName: "ComId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_empId",
                table: "Attendances",
                newName: "IX_Attendances_EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Company_ComId",
                table: "Attendances",
                column: "ComId",
                principalTable: "Company",
                principalColumn: "ComId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Employees_EmpId",
                table: "Attendances",
                column: "EmpId",
                principalTable: "Employees",
                principalColumn: "EmpId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Company_ComId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Employees_EmpId",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "EmpId",
                table: "Attendances",
                newName: "empId");

            migrationBuilder.RenameColumn(
                name: "ComId",
                table: "Attendances",
                newName: "comId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_EmpId",
                table: "Attendances",
                newName: "IX_Attendances_empId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Company_comId",
                table: "Attendances",
                column: "comId",
                principalTable: "Company",
                principalColumn: "ComId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Employees_empId",
                table: "Attendances",
                column: "empId",
                principalTable: "Employees",
                principalColumn: "EmpId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
