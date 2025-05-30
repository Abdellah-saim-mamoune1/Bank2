using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bankApI.Migrations
{
    /// <inheritdoc />
    public partial class sewrt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientXNotifications_Notification_NotificationId",
                table: "ClientXNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_NotificationsTypes_TypeId",
                table: "Notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notification",
                table: "Notification");

            migrationBuilder.RenameTable(
                name: "Notification",
                newName: "Notifications");

            migrationBuilder.RenameIndex(
                name: "IX_Notification_TypeId",
                table: "Notifications",
                newName: "IX_Notifications_TypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientXNotifications_Notifications_NotificationId",
                table: "ClientXNotifications",
                column: "NotificationId",
                principalTable: "Notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_NotificationsTypes_TypeId",
                table: "Notifications",
                column: "TypeId",
                principalTable: "NotificationsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientXNotifications_Notifications_NotificationId",
                table: "ClientXNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_NotificationsTypes_TypeId",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "Notification");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_TypeId",
                table: "Notification",
                newName: "IX_Notification_TypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notification",
                table: "Notification",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientXNotifications_Notification_NotificationId",
                table: "ClientXNotifications",
                column: "NotificationId",
                principalTable: "Notification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_NotificationsTypes_TypeId",
                table: "Notification",
                column: "TypeId",
                principalTable: "NotificationsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
