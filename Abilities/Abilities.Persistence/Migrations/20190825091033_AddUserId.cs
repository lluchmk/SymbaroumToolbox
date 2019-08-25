using Microsoft.EntityFrameworkCore.Migrations;

namespace Abilities.Persistence.Migrations
{
    public partial class AddUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Rituals",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MysticalPowers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Abilities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Rituals");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MysticalPowers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Abilities");
        }
    }
}
