using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Abilities.Persistence.Migrations
{
    public partial class SimplifyModelsStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MysticalPowers");

            migrationBuilder.DropTable(
                name: "Rituals");

            migrationBuilder.AlterColumn<int>(
                name: "NoviceType",
                table: "Abilities",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MasterType",
                table: "Abilities",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AdeptType",
                table: "Abilities",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Abilities",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "Abilities",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tradition",
                table: "Abilities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "Tradition",
                table: "Abilities");

            migrationBuilder.AlterColumn<int>(
                name: "NoviceType",
                table: "Abilities",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MasterType",
                table: "Abilities",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdeptType",
                table: "Abilities",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "MysticalPowers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(nullable: true),
                    Material = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    AdeptDescription = table.Column<string>(nullable: true),
                    AdeptType = table.Column<int>(nullable: false),
                    MasterDescription = table.Column<string>(nullable: true),
                    MasterType = table.Column<int>(nullable: false),
                    NoviceDescription = table.Column<string>(nullable: true),
                    NoviceType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MysticalPowers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rituals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Tradition = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rituals", x => x.Id);
                });
        }
    }
}
