using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Migrations
{
    /// <inheritdoc />
    public partial class emp_Man_relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Employees_managerId",
                table: "Employees",
                column: "managerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_managerId",
                table: "Employees",
                column: "managerId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_managerId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_managerId",
                table: "Employees");
        }
    }
}
