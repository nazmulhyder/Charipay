using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charipay.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatecapitalletteratthefrontforvolunteerprogressupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_volunteerProgressUpdates_VolunteerUsers_VolunteerUserId",
                table: "volunteerProgressUpdates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_volunteerProgressUpdates",
                table: "volunteerProgressUpdates");

            migrationBuilder.RenameTable(
                name: "volunteerProgressUpdates",
                newName: "VolunteerProgressUpdates");

            migrationBuilder.RenameIndex(
                name: "IX_volunteerProgressUpdates_VolunteerUserId",
                table: "VolunteerProgressUpdates",
                newName: "IX_VolunteerProgressUpdates_VolunteerUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VolunteerProgressUpdates",
                table: "VolunteerProgressUpdates",
                column: "VolunteerProgressUpdateId");

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerProgressUpdates_VolunteerUsers_VolunteerUserId",
                table: "VolunteerProgressUpdates",
                column: "VolunteerUserId",
                principalTable: "VolunteerUsers",
                principalColumn: "VolunteerUserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerProgressUpdates_VolunteerUsers_VolunteerUserId",
                table: "VolunteerProgressUpdates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VolunteerProgressUpdates",
                table: "VolunteerProgressUpdates");

            migrationBuilder.RenameTable(
                name: "VolunteerProgressUpdates",
                newName: "volunteerProgressUpdates");

            migrationBuilder.RenameIndex(
                name: "IX_VolunteerProgressUpdates_VolunteerUserId",
                table: "volunteerProgressUpdates",
                newName: "IX_volunteerProgressUpdates_VolunteerUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_volunteerProgressUpdates",
                table: "volunteerProgressUpdates",
                column: "VolunteerProgressUpdateId");

            migrationBuilder.AddForeignKey(
                name: "FK_volunteerProgressUpdates_VolunteerUsers_VolunteerUserId",
                table: "volunteerProgressUpdates",
                column: "VolunteerUserId",
                principalTable: "VolunteerUsers",
                principalColumn: "VolunteerUserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
