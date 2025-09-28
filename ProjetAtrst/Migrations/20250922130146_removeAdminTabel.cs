using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetAtrst.Migrations
{
    /// <inheritdoc />
    public partial class removeAdminTabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_AspNetUsers_Id",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Admins_ApprovedByAdminId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Associates_Admins_ApprovedByAdminId",
                table: "Associates");

            migrationBuilder.DropForeignKey(
                name: "FK_Partners_Admins_ApprovedByAdminId",
                table: "Partners");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Admins_ApprovedByAdminId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Partners_ApprovedByAdminId",
                table: "Partners");

            migrationBuilder.DropIndex(
                name: "IX_Associates_ApprovedByAdminId",
                table: "Associates");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ApprovedByAdminId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedByAdminId",
                table: "Partners",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedByAdminId",
                table: "Associates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedByAdminId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Admins",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AdminId",
                table: "AspNetUsers",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserId",
                table: "Admins",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_AspNetUsers_UserId",
                table: "Admins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Admins_AdminId",
                table: "AspNetUsers",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Admins_ApprovedByAdminId",
                table: "Projects",
                column: "ApprovedByAdminId",
                principalTable: "Admins",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_AspNetUsers_UserId",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Admins_AdminId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Admins_ApprovedByAdminId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AdminId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Admins_UserId",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Admins");

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedByAdminId",
                table: "Partners",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedByAdminId",
                table: "Associates",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedByAdminId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partners_ApprovedByAdminId",
                table: "Partners",
                column: "ApprovedByAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Associates_ApprovedByAdminId",
                table: "Associates",
                column: "ApprovedByAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ApprovedByAdminId",
                table: "AspNetUsers",
                column: "ApprovedByAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_AspNetUsers_Id",
                table: "Admins",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Admins_ApprovedByAdminId",
                table: "AspNetUsers",
                column: "ApprovedByAdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Associates_Admins_ApprovedByAdminId",
                table: "Associates",
                column: "ApprovedByAdminId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Partners_Admins_ApprovedByAdminId",
                table: "Partners",
                column: "ApprovedByAdminId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Admins_ApprovedByAdminId",
                table: "Projects",
                column: "ApprovedByAdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
