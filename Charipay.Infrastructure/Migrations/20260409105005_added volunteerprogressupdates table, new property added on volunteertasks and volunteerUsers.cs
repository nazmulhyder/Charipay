using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charipay.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedvolunteerprogressupdatestablenewpropertyaddedonvolunteertasksandvolunteerUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerTasks_Campaigns_CampaignId",
                table: "VolunteerTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerUsers_Users_UserId",
                table: "VolunteerUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerUsers_VolunteerTasks_VolunteerTaskId",
                table: "VolunteerUsers");

            migrationBuilder.AddColumn<string>(
                name: "AdminNote",
                table: "VolunteerUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvailabilityNote",
                table: "VolunteerUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "VolunteerUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewedAt",
                table: "VolunteerUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReviewedByAdminId",
                table: "VolunteerUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartedAt",
                table: "VolunteerUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "VolunteerUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VolunteerMessage",
                table: "VolunteerUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instructions",
                table: "VolunteerTasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "VolunteerTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Requirements",
                table: "VolunteerTasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupportType",
                table: "VolunteerTasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "volunteerProgressUpdates",
                columns: table => new
                {
                    VolunteerProgressUpdateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdateText = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VolunteerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_volunteerProgressUpdates", x => x.VolunteerProgressUpdateId);
                    table.ForeignKey(
                        name: "FK_volunteerProgressUpdates_VolunteerUsers_VolunteerUserId",
                        column: x => x.VolunteerUserId,
                        principalTable: "VolunteerUsers",
                        principalColumn: "VolunteerUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_volunteerProgressUpdates_VolunteerUserId",
                table: "volunteerProgressUpdates",
                column: "VolunteerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerTasks_Campaigns_CampaignId",
                table: "VolunteerTasks",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerUsers_Users_UserId",
                table: "VolunteerUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerUsers_VolunteerTasks_VolunteerTaskId",
                table: "VolunteerUsers",
                column: "VolunteerTaskId",
                principalTable: "VolunteerTasks",
                principalColumn: "VolunteerTaskId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerTasks_Campaigns_CampaignId",
                table: "VolunteerTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerUsers_Users_UserId",
                table: "VolunteerUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerUsers_VolunteerTasks_VolunteerTaskId",
                table: "VolunteerUsers");

            migrationBuilder.DropTable(
                name: "volunteerProgressUpdates");

            migrationBuilder.DropColumn(
                name: "AdminNote",
                table: "VolunteerUsers");

            migrationBuilder.DropColumn(
                name: "AvailabilityNote",
                table: "VolunteerUsers");

            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "VolunteerUsers");

            migrationBuilder.DropColumn(
                name: "ReviewedAt",
                table: "VolunteerUsers");

            migrationBuilder.DropColumn(
                name: "ReviewedByAdminId",
                table: "VolunteerUsers");

            migrationBuilder.DropColumn(
                name: "StartedAt",
                table: "VolunteerUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "VolunteerUsers");

            migrationBuilder.DropColumn(
                name: "VolunteerMessage",
                table: "VolunteerUsers");

            migrationBuilder.DropColumn(
                name: "Instructions",
                table: "VolunteerTasks");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "VolunteerTasks");

            migrationBuilder.DropColumn(
                name: "Requirements",
                table: "VolunteerTasks");

            migrationBuilder.DropColumn(
                name: "SupportType",
                table: "VolunteerTasks");

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerTasks_Campaigns_CampaignId",
                table: "VolunteerTasks",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerUsers_Users_UserId",
                table: "VolunteerUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerUsers_VolunteerTasks_VolunteerTaskId",
                table: "VolunteerUsers",
                column: "VolunteerTaskId",
                principalTable: "VolunteerTasks",
                principalColumn: "VolunteerTaskId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
