using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bankApI.Migrations
{
    /// <inheritdoc />
    public partial class changingtranasactionhistoryfk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionsHistory_Accounts_TypeId",
                table: "TransactionsHistory");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionsHistory_AccountId",
                table: "TransactionsHistory",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionsHistory_Accounts_AccountId",
                table: "TransactionsHistory",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionsHistory_Accounts_AccountId",
                table: "TransactionsHistory");

            migrationBuilder.DropIndex(
                name: "IX_TransactionsHistory_AccountId",
                table: "TransactionsHistory");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionsHistory_Accounts_TypeId",
                table: "TransactionsHistory",
                column: "TypeId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
