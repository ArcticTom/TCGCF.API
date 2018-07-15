using Microsoft.EntityFrameworkCore.Migrations;

namespace TCGCF.API.Migrations
{
    public partial class DataImport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "PrintNumber",
                table: "Cards");

            migrationBuilder.AddColumn<string>(
                name: "CardName",
                table: "Language",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LanguageName",
                table: "Language",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Toughness",
                table: "Cards",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Power",
                table: "Cards",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Cards",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardName",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "LanguageName",
                table: "Language");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Cards");

            migrationBuilder.AddColumn<int>(
                name: "Name",
                table: "Language",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Toughness",
                table: "Cards",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "Power",
                table: "Cards",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "PrintNumber",
                table: "Cards",
                nullable: false,
                defaultValue: 0);
        }
    }
}
