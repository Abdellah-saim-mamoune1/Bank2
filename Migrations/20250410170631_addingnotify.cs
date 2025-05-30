using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bankApI.Migrations
{
    /// <inheritdoc />
    public partial class addingnotify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientXNotifications_CNotification_NotificationId",
                table: "ClientXNotifications");

            migrationBuilder.DropTable(
                name: "CNotification");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "ClientXNotifications",
                type: "date",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.CreateTable(
                name: "NotificationsTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationsTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Isviewed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_NotificationsTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "NotificationsTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_TypeId",
                table: "Notification",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientXNotifications_Notification_NotificationId",
                table: "ClientXNotifications",
                column: "NotificationId",
                principalTable: "Notification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientXNotifications_Notification_NotificationId",
                table: "ClientXNotifications");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "NotificationsTypes");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "ClientXNotifications",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.CreateTable(
                name: "CNotification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Isviewed = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNotification", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ClientXNotifications_CNotification_NotificationId",
                table: "ClientXNotifications",
                column: "NotificationId",
                principalTable: "CNotification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
