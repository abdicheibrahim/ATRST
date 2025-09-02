using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetAtrst.Migrations
{
    /// <inheritdoc />
    public partial class AddAssociate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssociateId",
                table: "ProjectRequests",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AssociateId",
                table: "ProjectMemberships",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "AssociateId",
                table: "Notifications",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Associate",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Diploma = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Speciality = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MemberParticipation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PartenaireApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    ApprovedByAdminId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Associate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Associate_Admins_ApprovedByAdminId",
                        column: x => x.ApprovedByAdminId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Associate_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRequests_AssociateId",
                table: "ProjectRequests",
                column: "AssociateId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMemberships_AssociateId",
                table: "ProjectMemberships",
                column: "AssociateId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AssociateId",
                table: "Notifications",
                column: "AssociateId");

            migrationBuilder.CreateIndex(
                name: "IX_Associate_ApprovedByAdminId",
                table: "Associate",
                column: "ApprovedByAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Associate_AssociateId",
                table: "Notifications",
                column: "AssociateId",
                principalTable: "Associate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMemberships_Associate_AssociateId",
                table: "ProjectMemberships",
                column: "AssociateId",
                principalTable: "Associate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequests_Associate_AssociateId",
                table: "ProjectRequests",
                column: "AssociateId",
                principalTable: "Associate",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Associate_AssociateId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMemberships_Associate_AssociateId",
                table: "ProjectMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequests_Associate_AssociateId",
                table: "ProjectRequests");

            migrationBuilder.DropTable(
                name: "Associate");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRequests_AssociateId",
                table: "ProjectRequests");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMemberships_AssociateId",
                table: "ProjectMemberships");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_AssociateId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "AssociateId",
                table: "ProjectRequests");

            migrationBuilder.DropColumn(
                name: "AssociateId",
                table: "ProjectMemberships");

            migrationBuilder.DropColumn(
                name: "AssociateId",
                table: "Notifications");
        }
    }
}
