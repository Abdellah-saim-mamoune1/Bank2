using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bankApI.Migrations
{
    /// <inheritdoc />
    public partial class ffjj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_ClientType_TypeId",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "ClientType");

            migrationBuilder.DropIndex(
                name: "IX_Clients_TypeId",
                table: "Clients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_TypeId",
                table: "Clients",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_ClientType_TypeId",
                table: "Clients",
                column: "TypeId",
                principalTable: "ClientType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
