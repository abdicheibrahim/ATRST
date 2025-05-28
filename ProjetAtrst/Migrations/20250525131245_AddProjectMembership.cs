using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetAtrst.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectMembership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMembers_Projects_JoinedProjectId",
                table: "ProjectMembers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectMembers_JoinedProjectId",
                table: "ProjectMembers");

            migrationBuilder.DropColumn(
                name: "JoinedProjectId",
                table: "ProjectMembers");

            migrationBuilder.CreateTable(
                name: "ProjectMembership",
                columns: table => new
                {
                    MemberId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMembership", x => new { x.MemberId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ProjectMembership_ProjectMembers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "ProjectMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectMembership_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMembership_ProjectId",
                table: "ProjectMembership",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectMembership");

            migrationBuilder.AddColumn<int>(
                name: "JoinedProjectId",
                table: "ProjectMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMembers_JoinedProjectId",
                table: "ProjectMembers",
                column: "JoinedProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMembers_Projects_JoinedProjectId",
                table: "ProjectMembers",
                column: "JoinedProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
