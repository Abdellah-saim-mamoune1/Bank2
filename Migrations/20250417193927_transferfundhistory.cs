using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bankApI.Migrations
{
    /// <inheritdoc />
    public partial class transferfundhistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransferFundHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderAccountId = table.Column<int>(type: "int", nullable: false),
                    RecieverAccountId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferFundHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferFundHistory_Accounts_RecieverAccountId",
                        column: x => x.RecieverAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferFundHistory_Accounts_SenderAccountId",
                        column: x => x.SenderAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferFundHistory_RecieverAccountId",
                table: "TransferFundHistory",
                column: "RecieverAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferFundHistory_SenderAccountId",
                table: "TransferFundHistory",
                column: "SenderAccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferFundHistory");
        }
    }
}
