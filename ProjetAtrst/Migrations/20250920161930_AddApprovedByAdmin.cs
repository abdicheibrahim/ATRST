using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetAtrst.Migrations
{
    /// <inheritdoc />
    public partial class AddApprovedByAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Researchers_Admins_ApprovedByAdminId",
                table: "Researchers");

            migrationBuilder.DropIndex(
                name: "IX_Researchers_ApprovedByAdminId",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "ApprovedByAdminId",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "ResearcherApprovalStatus",
                table: "Researchers");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedByAdminId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserApprovalStatus",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ApprovedByAdminId",
                table: "AspNetUsers",
                column: "ApprovedByAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Admins_ApprovedByAdminId",
                table: "AspNetUsers",
                column: "ApprovedByAdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Admins_ApprovedByAdminId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ApprovedByAdminId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApprovedByAdminId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserApprovalStatus",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedByAdminId",
                table: "Researchers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResearcherApprovalStatus",
                table: "Researchers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Researchers_ApprovedByAdminId",
                table: "Researchers",
                column: "ApprovedByAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Researchers_Admins_ApprovedByAdminId",
                table: "Researchers",
                column: "ApprovedByAdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
