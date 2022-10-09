using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Battleground.Api.Migrations
{
    public partial class PokemonBattleUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattlePokemons_Players_PlayerId",
                table: "BattlePokemons");

            migrationBuilder.DropIndex(
                name: "IX_BattlePokemons_PlayerId",
                table: "BattlePokemons");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "BattlePokemons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "BattlePokemons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BattlePokemons_PlayerId",
                table: "BattlePokemons",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BattlePokemons_Players_PlayerId",
                table: "BattlePokemons",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
