using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bankApI.Migrations
{
    /// <inheritdoc />
    public partial class removingroleclient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Roles_RoleTypeId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_RoleTypeId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "RoleTypeId",
                table: "Clients");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_TypeId",
                table: "Clients",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Roles_TypeId",
                table: "Clients",
                column: "TypeId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Roles_TypeId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_TypeId",
                table: "Clients");

            migrationBuilder.AddColumn<int>(
                name: "RoleTypeId",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_RoleTypeId",
                table: "Clients",
                column: "RoleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Roles_RoleTypeId",
                table: "Clients",
                column: "RoleTypeId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
