using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Battleground.Api.Migrations
{
    public partial class FixPlayerInventories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlyerInventories",
                table: "PlyerInventories");

            migrationBuilder.RenameTable(
                name: "PlyerInventories",
                newName: "PlayerInventories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerInventories",
                table: "PlayerInventories",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerInventories",
                table: "PlayerInventories");

            migrationBuilder.RenameTable(
                name: "PlayerInventories",
                newName: "PlyerInventories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlyerInventories",
                table: "PlyerInventories",
                column: "Id");
        }
    }
}
