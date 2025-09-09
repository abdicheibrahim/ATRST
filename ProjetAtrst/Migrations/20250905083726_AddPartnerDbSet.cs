using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetAtrst.Migrations
{
    /// <inheritdoc />
    public partial class AddPartnerDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Associate_Admins_ApprovedByAdminId",
                table: "Associate");

            migrationBuilder.DropForeignKey(
                name: "FK_Associate_AspNetUsers_Id",
                table: "Associate");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Associate_AssociateId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Partner_PartnerId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Partner_Admins_ApprovedByAdminId",
                table: "Partner");

            migrationBuilder.DropForeignKey(
                name: "FK_Partner_AspNetUsers_Id",
                table: "Partner");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMemberships_Associate_AssociateId",
                table: "ProjectMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMemberships_Partner_PartnerId",
                table: "ProjectMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequests_Associate_AssociateId",
                table: "ProjectRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequests_Partner_PartnerId",
                table: "ProjectRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Partner",
                table: "Partner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Associate",
                table: "Associate");

            migrationBuilder.RenameTable(
                name: "Partner",
                newName: "Partners");

            migrationBuilder.RenameTable(
                name: "Associate",
                newName: "Associates");

            migrationBuilder.RenameIndex(
                name: "IX_Partner_ApprovedByAdminId",
                table: "Partners",
                newName: "IX_Partners_ApprovedByAdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Associate_ApprovedByAdminId",
                table: "Associates",
                newName: "IX_Associates_ApprovedByAdminId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Partners",
                table: "Partners",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Associates",
                table: "Associates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Associates_Admins_ApprovedByAdminId",
                table: "Associates",
                column: "ApprovedByAdminId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Associates_AspNetUsers_Id",
                table: "Associates",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Partners_Admins_ApprovedByAdminId",
                table: "Partners",
                column: "ApprovedByAdminId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Partners_AspNetUsers_Id",
                table: "Partners",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMemberships_Associates_AssociateId",
                table: "ProjectMemberships",
                column: "AssociateId",
                principalTable: "Associates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMemberships_Partners_PartnerId",
                table: "ProjectMemberships",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequests_Associates_AssociateId",
                table: "ProjectRequests",
                column: "AssociateId",
                principalTable: "Associates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequests_Partners_PartnerId",
                table: "ProjectRequests",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Associates_Admins_ApprovedByAdminId",
                table: "Associates");

            migrationBuilder.DropForeignKey(
                name: "FK_Associates_AspNetUsers_Id",
                table: "Associates");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Associates_AssociateId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Partners_PartnerId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Partners_Admins_ApprovedByAdminId",
                table: "Partners");

            migrationBuilder.DropForeignKey(
                name: "FK_Partners_AspNetUsers_Id",
                table: "Partners");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMemberships_Associates_AssociateId",
                table: "ProjectMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMemberships_Partners_PartnerId",
                table: "ProjectMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequests_Associates_AssociateId",
                table: "ProjectRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequests_Partners_PartnerId",
                table: "ProjectRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Partners",
                table: "Partners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Associates",
                table: "Associates");

            migrationBuilder.RenameTable(
                name: "Partners",
                newName: "Partner");

            migrationBuilder.RenameTable(
                name: "Associates",
                newName: "Associate");

            migrationBuilder.RenameIndex(
                name: "IX_Partners_ApprovedByAdminId",
                table: "Partner",
                newName: "IX_Partner_ApprovedByAdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Associates_ApprovedByAdminId",
                table: "Associate",
                newName: "IX_Associate_ApprovedByAdminId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Partner",
                table: "Partner",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Associate",
                table: "Associate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Associate_Admins_ApprovedByAdminId",
                table: "Associate",
                column: "ApprovedByAdminId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Associate_AspNetUsers_Id",
                table: "Associate",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Associate_AssociateId",
                table: "Notifications",
                column: "AssociateId",
                principalTable: "Associate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Partner_PartnerId",
                table: "Notifications",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Partner_Admins_ApprovedByAdminId",
                table: "Partner",
                column: "ApprovedByAdminId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Partner_AspNetUsers_Id",
                table: "Partner",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMemberships_Associate_AssociateId",
                table: "ProjectMemberships",
                column: "AssociateId",
                principalTable: "Associate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMemberships_Partner_PartnerId",
                table: "ProjectMemberships",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequests_Associate_AssociateId",
                table: "ProjectRequests",
                column: "AssociateId",
                principalTable: "Associate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequests_Partner_PartnerId",
                table: "ProjectRequests",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "Id");
        }
    }
}
