using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Battleground.Api.Migrations
{
    public partial class BattleConnectors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Battles_StatusId",
                table: "Battles",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_BattlePokemons_BattleId",
                table: "BattlePokemons",
                column: "BattleId");

            migrationBuilder.CreateIndex(
                name: "IX_BattlePlayers_BattlesId",
                table: "BattlePlayers",
                column: "BattlesId");

            migrationBuilder.CreateIndex(
                name: "IX_BattlePlayers_PlayerInMatchId",
                table: "BattlePlayers",
                column: "PlayerInMatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Attacks_BattlePokemonId",
                table: "Attacks",
                column: "BattlePokemonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attacks_BattlePokemons_BattlePokemonId",
                table: "Attacks",
                column: "BattlePokemonId",
                principalTable: "BattlePokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BattlePlayers_Battles_BattlesId",
                table: "BattlePlayers",
                column: "BattlesId",
                principalTable: "Battles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BattlePlayers_Players_PlayerInMatchId",
                table: "BattlePlayers",
                column: "PlayerInMatchId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BattlePokemons_Battles_BattleId",
                table: "BattlePokemons",
                column: "BattleId",
                principalTable: "Battles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_BattleStatuses_StatusId",
                table: "Battles",
                column: "StatusId",
                principalTable: "BattleStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attacks_BattlePokemons_BattlePokemonId",
                table: "Attacks");

            migrationBuilder.DropForeignKey(
                name: "FK_BattlePlayers_Battles_BattlesId",
                table: "BattlePlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_BattlePlayers_Players_PlayerInMatchId",
                table: "BattlePlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_BattlePokemons_Battles_BattleId",
                table: "BattlePokemons");

            migrationBuilder.DropForeignKey(
                name: "FK_Battles_BattleStatuses_StatusId",
                table: "Battles");

            migrationBuilder.DropIndex(
                name: "IX_Battles_StatusId",
                table: "Battles");

            migrationBuilder.DropIndex(
                name: "IX_BattlePokemons_BattleId",
                table: "BattlePokemons");

            migrationBuilder.DropIndex(
                name: "IX_BattlePlayers_BattlesId",
                table: "BattlePlayers");

            migrationBuilder.DropIndex(
                name: "IX_BattlePlayers_PlayerInMatchId",
                table: "BattlePlayers");

            migrationBuilder.DropIndex(
                name: "IX_Attacks_BattlePokemonId",
                table: "Attacks");
        }
    }
}
