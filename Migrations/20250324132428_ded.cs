using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bankApI.Migrations
{
    /// <inheritdoc />
    public partial class ded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAccount_Employees_EmployeeId",
                table: "EmployeeAccount");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAccount_Persons_EmployeeId",
                table: "EmployeeAccount",
                column: "EmployeeId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAccount_Persons_EmployeeId",
                table: "EmployeeAccount");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAccount_Employees_EmployeeId",
                table: "EmployeeAccount",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
