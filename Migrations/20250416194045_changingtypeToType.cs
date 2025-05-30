using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bankApI.Migrations
{
    /// <inheritdoc />
    public partial class changingtypeToType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionsHistory_Accounts_Type",
                table: "TransactionsHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionsHistory_TransactionsTypes_Type",
                table: "TransactionsHistory");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "TransactionsHistory",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionsHistory_Type",
                table: "TransactionsHistory",
                newName: "IX_TransactionsHistory_TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionsHistory_Accounts_TypeId",
                table: "TransactionsHistory",
                column: "TypeId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionsHistory_TransactionsTypes_TypeId",
                table: "TransactionsHistory",
                column: "TypeId",
                principalTable: "TransactionsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionsHistory_Accounts_TypeId",
                table: "TransactionsHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionsHistory_TransactionsTypes_TypeId",
                table: "TransactionsHistory");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "TransactionsHistory",
                newName: "Type");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionsHistory_TypeId",
                table: "TransactionsHistory",
                newName: "IX_TransactionsHistory_Type");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionsHistory_Accounts_Type",
                table: "TransactionsHistory",
                column: "Type",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionsHistory_TransactionsTypes_Type",
                table: "TransactionsHistory",
                column: "Type",
                principalTable: "TransactionsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
