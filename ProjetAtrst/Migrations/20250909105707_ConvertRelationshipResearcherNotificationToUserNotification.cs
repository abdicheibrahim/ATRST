using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetAtrst.Migrations
{
    /// <inheritdoc />
    public partial class ConvertRelationshipResearcherNotificationToUserNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Associates_AssociateId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Partners_PartnerId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Researchers_UserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_AssociateId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_PartnerId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "AssociateId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "Notifications");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "AssociateId",
                table: "Notifications",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PartnerId",
                table: "Notifications",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AssociateId",
                table: "Notifications",
                column: "AssociateId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PartnerId",
                table: "Notifications",
                column: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Associates_AssociateId",
                table: "Notifications",
                column: "AssociateId",
                principalTable: "Associates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Partners_PartnerId",
                table: "Notifications",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Researchers_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "Researchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
