using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetAtrst.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreProjectFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Axis",
                table: "Projects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CurrentState",
                table: "Projects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Domain",
                table: "Projects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "DurationInMonths",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ExpectedResults",
                table: "Projects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "HostInstitution",
                table: "Projects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Impact",
                table: "Projects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Keywords",
                table: "Projects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Methodology",
                table: "Projects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Motivation",
                table: "Projects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Nature",
                table: "Projects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ReferencesJson",
                table: "Projects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SocioEconomicPartner",
                table: "Projects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TRL",
                table: "Projects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TargetSectors",
                table: "Projects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "Projects",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Axis",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CurrentState",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Domain",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DurationInMonths",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ExpectedResults",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "HostInstitution",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Impact",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Keywords",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Methodology",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Motivation",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Nature",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ReferencesJson",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SocioEconomicPartner",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TRL",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TargetSectors",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Theme",
                table: "Projects");
        }
    }
}
