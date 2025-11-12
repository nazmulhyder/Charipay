using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charipay.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class campaigntablemadechanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "Campaigns",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Campaigns",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Campaigns",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Campaigns");
        }
    }
}
