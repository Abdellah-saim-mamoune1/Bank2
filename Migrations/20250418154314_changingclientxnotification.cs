using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bankApI.Migrations
{
    /// <inheritdoc />
    public partial class changingclientxnotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientXNotifications_Clients_ClientId",
                table: "ClientXNotifications");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "ClientXNotifications",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientXNotifications_ClientId",
                table: "ClientXNotifications",
                newName: "IX_ClientXNotifications_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientXNotifications_Accounts_AccountId",
                table: "ClientXNotifications",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientXNotifications_Accounts_AccountId",
                table: "ClientXNotifications");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "ClientXNotifications",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientXNotifications_AccountId",
                table: "ClientXNotifications",
                newName: "IX_ClientXNotifications_ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientXNotifications_Clients_ClientId",
                table: "ClientXNotifications",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
