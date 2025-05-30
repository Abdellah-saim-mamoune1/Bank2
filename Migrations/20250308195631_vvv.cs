using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bankApI.Migrations
{
    /// <inheritdoc />
    public partial class vvv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Persons_PersonId",
                table: "Wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets");

            migrationBuilder.RenameTable(
                name: "Wallets",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_Wallets_PersonId",
                table: "Accounts",
                newName: "IX_Accounts_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Persons_PersonId",
                table: "Accounts",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Persons_PersonId",
                table: "Accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Wallets");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_PersonId",
                table: "Wallets",
                newName: "IX_Wallets_PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Persons_PersonId",
                table: "Wallets",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
