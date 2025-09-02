using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetAtrst.Migrations
{
    /// <inheritdoc />
    public partial class AddPartenaire : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMemberships_Researchers_ResearcherId",
                table: "ProjectMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequests_Researchers_ReceiverId",
                table: "ProjectRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequests_Researchers_SenderId",
                table: "ProjectRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectMemberships",
                table: "ProjectMemberships");

            migrationBuilder.AddColumn<string>(
                name: "ResearcherId",
                table: "ProjectRequests",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ResearcherId",
                table: "ProjectMemberships",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ProjectMemberships",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectMemberships",
                table: "ProjectMemberships",
                columns: new[] { "UserId", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRequests_ResearcherId",
                table: "ProjectRequests",
                column: "ResearcherId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMemberships_ResearcherId",
                table: "ProjectMemberships",
                column: "ResearcherId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMemberships_AspNetUsers_UserId",
                table: "ProjectMemberships",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMemberships_Researchers_ResearcherId",
                table: "ProjectMemberships",
                column: "ResearcherId",
                principalTable: "Researchers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequests_AspNetUsers_ReceiverId",
                table: "ProjectRequests",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequests_AspNetUsers_SenderId",
                table: "ProjectRequests",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequests_Researchers_ResearcherId",
                table: "ProjectRequests",
                column: "ResearcherId",
                principalTable: "Researchers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMemberships_AspNetUsers_UserId",
                table: "ProjectMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMemberships_Researchers_ResearcherId",
                table: "ProjectMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequests_AspNetUsers_ReceiverId",
                table: "ProjectRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequests_AspNetUsers_SenderId",
                table: "ProjectRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRequests_Researchers_ResearcherId",
                table: "ProjectRequests");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRequests_ResearcherId",
                table: "ProjectRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectMemberships",
                table: "ProjectMemberships");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMemberships_ResearcherId",
                table: "ProjectMemberships");

            migrationBuilder.DropColumn(
                name: "ResearcherId",
                table: "ProjectRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProjectMemberships");

            migrationBuilder.UpdateData(
                table: "ProjectMemberships",
                keyColumn: "ResearcherId",
                keyValue: null,
                column: "ResearcherId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ResearcherId",
                table: "ProjectMemberships",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectMemberships",
                table: "ProjectMemberships",
                columns: new[] { "ResearcherId", "ProjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMemberships_Researchers_ResearcherId",
                table: "ProjectMemberships",
                column: "ResearcherId",
                principalTable: "Researchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequests_Researchers_ReceiverId",
                table: "ProjectRequests",
                column: "ReceiverId",
                principalTable: "Researchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRequests_Researchers_SenderId",
                table: "ProjectRequests",
                column: "SenderId",
                principalTable: "Researchers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
