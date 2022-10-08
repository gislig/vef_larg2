using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Battleground.Api.Migrations
{
    public partial class PokemonIdentifierAttack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PokemonIdentifier",
                table: "Attacks",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PokemonIdentifier",
                table: "Attacks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
