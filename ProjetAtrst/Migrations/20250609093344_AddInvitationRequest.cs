using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetAtrst.Migrations
{
    /// <inheritdoc />
    public partial class AddInvitationRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "InvitationRequests");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "InvitationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "InvitationRequests");

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "InvitationRequests",
                type: "tinyint(1)",
                nullable: true);
        }
    }
}
