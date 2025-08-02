using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetAtrst.Migrations
{
    /// <inheritdoc />
    public partial class AddPartnerFieldsToResearcher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mobile",
                table: "Researchers",
                newName: "SocioEconomicContributions");

            migrationBuilder.RenameColumn(
                name: "IsApprovedByAdmin",
                table: "Researchers",
                newName: "WantsToContributeAsPartner");

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "WantsToContributeAsPartner",
                table: "Researchers",
                newName: "IsApprovedByAdmin");

            migrationBuilder.RenameColumn(
                name: "SocioEconomicContributions",
                table: "Researchers",
                newName: "Mobile");
        }
    }
}
