using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetAtrst.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertiesInResearcher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Researchers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Diploma",
                table: "Researchers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Establishment",
                table: "Researchers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FirstNameAr",
                table: "Researchers",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Researchers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "Researchers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LastNameAr",
                table: "Researchers",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Researchers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Speciality",
                table: "Researchers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Researchers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "DipDate",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "DipInstitution",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "Diploma",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "Establishment",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "FirstNameAr",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "LastNameAr",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "Speciality",
                table: "Researchers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Researchers");
        }
    }
}
