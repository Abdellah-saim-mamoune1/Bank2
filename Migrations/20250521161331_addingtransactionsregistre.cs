using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bankApI.Migrations
{
    /// <inheritdoc />
    public partial class addingtransactionsregistre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRegistres_Clients_ReceiverClientId",
                table: "TransactionRegistres");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRegistres_Clients_SenderClientId",
                table: "TransactionRegistres");

            migrationBuilder.DropTable(
                name: "Deposits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionRegistres",
                table: "TransactionRegistres");

            migrationBuilder.RenameTable(
                name: "TransactionRegistres",
                newName: "TransactionRegistre");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionRegistres_SenderClientId",
                table: "TransactionRegistre",
                newName: "IX_TransactionRegistre_SenderClientId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionRegistres_ReceiverClientId",
                table: "TransactionRegistre",
                newName: "IX_TransactionRegistre_ReceiverClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionRegistre",
                table: "TransactionRegistre",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TransactionsRegistres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeAccountId = table.Column<int>(type: "int", nullable: false),
                    ClientAccountId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionsRegistres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionsRegistres_Accounts_ClientAccountId",
                        column: x => x.ClientAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionsRegistres_EmployeeAccount_EmployeeAccountId",
                        column: x => x.EmployeeAccountId,
                        principalTable: "EmployeeAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionsRegistres_ClientAccountId",
                table: "TransactionsRegistres",
                column: "ClientAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionsRegistres_EmployeeAccountId",
                table: "TransactionsRegistres",
                column: "EmployeeAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRegistre_Clients_ReceiverClientId",
                table: "TransactionRegistre",
                column: "ReceiverClientId",
                principalTable: "Clients",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRegistre_Clients_SenderClientId",
                table: "TransactionRegistre",
                column: "SenderClientId",
                principalTable: "Clients",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRegistre_Clients_ReceiverClientId",
                table: "TransactionRegistre");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionRegistre_Clients_SenderClientId",
                table: "TransactionRegistre");

            migrationBuilder.DropTable(
                name: "TransactionsRegistres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionRegistre",
                table: "TransactionRegistre");

            migrationBuilder.RenameTable(
                name: "TransactionRegistre",
                newName: "TransactionRegistres");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionRegistre_SenderClientId",
                table: "TransactionRegistres",
                newName: "IX_TransactionRegistres_SenderClientId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionRegistre_ReceiverClientId",
                table: "TransactionRegistres",
                newName: "IX_TransactionRegistres_ReceiverClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionRegistres",
                table: "TransactionRegistres",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Deposits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientAccountId = table.Column<int>(type: "int", nullable: false),
                    EmployeeAccountId = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRegistres_Clients_ReceiverClientId",
                table: "TransactionRegistres",
                column: "ReceiverClientId",
                principalTable: "Clients",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionRegistres_Clients_SenderClientId",
                table: "TransactionRegistres",
                column: "SenderClientId",
                principalTable: "Clients",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
