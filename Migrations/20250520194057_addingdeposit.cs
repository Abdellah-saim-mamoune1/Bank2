using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bankApI.Migrations
{
    /// <inheritdoc />
    public partial class addingdeposit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApprovedRequistesRejstres");

            migrationBuilder.DropTable(
                name: "NewClientRequists");

            migrationBuilder.CreateTable(
                name: "Deposits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeAccountId = table.Column<int>(type: "int", nullable: false),
                    ClientAccountId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deposits_Accounts_ClientAccountId",
                        column: x => x.ClientAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deposits_EmployeeAccount_EmployeeAccountId",
                        column: x => x.EmployeeAccountId,
                        principalTable: "EmployeeAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_ClientAccountId",
                table: "Deposits",
                column: "ClientAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_EmployeeAccountId",
                table: "Deposits",
                column: "EmployeeAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deposits");

            migrationBuilder.CreateTable(
                name: "ApprovedRequistesRejstres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedRequistesRejstres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApprovedRequistesRejstres_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "PersonId");
                    table.ForeignKey(
                        name: "FK_ApprovedRequistesRejstres_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateTable(
                name: "NewClientRequists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    BankEmailPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewClientRequists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRequistesRejstres_ClientId",
                table: "ApprovedRequistesRejstres",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApprovedRequistesRejstres_EmployeeId",
                table: "ApprovedRequistesRejstres",
                column: "EmployeeId");
        }
    }
}
