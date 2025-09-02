using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetAtrst.Migrations
{
    /// <inheritdoc />
    public partial class RenamePartenaireToPartner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Partenaire_PartenaireId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMemberships_Partenaire_PartenaireId",
                table: "ProjectMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequests_Partenaire_PartenaireId",
                table: "ProjectRequests");

            migrationBuilder.DropTable(
                name: "Partenaire");

            migrationBuilder.RenameColumn(
                name: "PartenaireId",
                table: "ProjectRequests",
                newName: "PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectRequests_PartenaireId",
                table: "ProjectRequests",
                newName: "IX_ProjectRequests_PartnerId");

            migrationBuilder.RenameColumn(
                name: "PartenaireId",
                table: "ProjectMemberships",
                newName: "PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectMemberships_PartenaireId",
                table: "ProjectMemberships",
                newName: "IX_ProjectMemberships_PartnerId");

            migrationBuilder.RenameColumn(
                name: "PartenaireId",
                table: "Notifications",
                newName: "PartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_PartenaireId",
                table: "Notifications",
                newName: "IX_Notifications_PartnerId");

            migrationBuilder.CreateTable(
                name: "Partner",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Baccalaureat = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Diploma = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Profession = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Speciality = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Establishment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PartnerResearchPrograms = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PartnerSocioEconomicWorks = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PartenaireApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    ApprovedByAdminId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partner_Admins_ApprovedByAdminId",
                        column: x => x.ApprovedByAdminId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Partner_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Partner_ApprovedByAdminId",
                table: "Partner",
                column: "ApprovedByAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Partner_PartnerId",
                table: "Notifications",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMemberships_Partner_PartnerId",
                table: "ProjectMemberships",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequests_Partner_PartnerId",
                table: "ProjectRequests",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Partner_PartnerId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMemberships_Partner_PartnerId",
                table: "ProjectMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequests_Partner_PartnerId",
                table: "ProjectRequests");

            migrationBuilder.DropTable(
                name: "Partner");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                table: "ProjectRequests",
                newName: "PartenaireId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectRequests_PartnerId",
                table: "ProjectRequests",
                newName: "IX_ProjectRequests_PartenaireId");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                table: "ProjectMemberships",
                newName: "PartenaireId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectMemberships_PartnerId",
                table: "ProjectMemberships",
                newName: "IX_ProjectMemberships_PartenaireId");

            migrationBuilder.RenameColumn(
                name: "PartnerId",
                table: "Notifications",
                newName: "PartenaireId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_PartnerId",
                table: "Notifications",
                newName: "IX_Notifications_PartenaireId");

            migrationBuilder.CreateTable(
                name: "Partenaire",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApprovedByAdminId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Baccalaureat = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Diploma = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Establishment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PartenaireApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    PartnerResearchPrograms = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PartnerSocioEconomicWorks = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Profession = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Speciality = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partenaire", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partenaire_Admins_ApprovedByAdminId",
                        column: x => x.ApprovedByAdminId,
                        principalTable: "Admins",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Partenaire_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Partenaire_ApprovedByAdminId",
                table: "Partenaire",
                column: "ApprovedByAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Partenaire_PartenaireId",
                table: "Notifications",
                column: "PartenaireId",
                principalTable: "Partenaire",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMemberships_Partenaire_PartenaireId",
                table: "ProjectMemberships",
                column: "PartenaireId",
                principalTable: "Partenaire",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequests_Partenaire_PartenaireId",
                table: "ProjectRequests",
                column: "PartenaireId",
                principalTable: "Partenaire",
                principalColumn: "Id");
        }
    }
}
