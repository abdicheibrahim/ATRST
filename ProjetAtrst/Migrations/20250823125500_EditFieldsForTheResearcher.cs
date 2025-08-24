using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetAtrst.Migrations
{
    /// <inheritdoc />
    public partial class EditFieldsForTheResearcher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DipDate",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "DipInstitution",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "SocioEconomicContributions",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "WantsToContributeAsPartner",
                table: "Researchers");

            migrationBuilder.RenameColumn(
                name: "Statut",
                table: "Researchers",
                newName: "ParticipationPrograms");

            migrationBuilder.AlterColumn<string>(
                name: "Establishment",
                table: "Researchers",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "RoleType",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleType",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ParticipationPrograms",
                table: "Researchers",
                newName: "Statut");

            migrationBuilder.AlterColumn<string>(
                name: "Establishment",
                table: "Researchers",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DipDate",
                table: "Researchers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DipInstitution",
                table: "Researchers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SocioEconomicContributions",
                table: "Researchers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "WantsToContributeAsPartner",
                table: "Researchers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
