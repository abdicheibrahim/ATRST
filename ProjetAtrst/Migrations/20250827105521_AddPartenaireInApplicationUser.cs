using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetAtrst.Migrations
{
    /// <inheritdoc />
    public partial class AddPartenaireInApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PartenaireId",
                table: "ProjectRequests",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PartenaireId",
                table: "ProjectMemberships",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PartenaireId",
                table: "Notifications",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Partenaire",
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
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsCompleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
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
                name: "IX_ProjectRequests_PartenaireId",
                table: "ProjectRequests",
                column: "PartenaireId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMemberships_PartenaireId",
                table: "ProjectMemberships",
                column: "PartenaireId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PartenaireId",
                table: "Notifications",
                column: "PartenaireId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_ProjectRequests_PartenaireId",
                table: "ProjectRequests");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMemberships_PartenaireId",
                table: "ProjectMemberships");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_PartenaireId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "PartenaireId",
                table: "ProjectRequests");

            migrationBuilder.DropColumn(
                name: "PartenaireId",
                table: "ProjectMemberships");

            migrationBuilder.DropColumn(
                name: "PartenaireId",
                table: "Notifications");
        }
    }
}
