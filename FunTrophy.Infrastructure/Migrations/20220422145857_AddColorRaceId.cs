using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunTrophy.Infrastructure.Migrations
{
    public partial class AddColorRaceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RaceId",
                table: "Colors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Colors_RaceId",
                table: "Colors",
                column: "RaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Colors_Races_RaceId",
                table: "Colors",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colors_Races_RaceId",
                table: "Colors");

            migrationBuilder.DropIndex(
                name: "IX_Colors_RaceId",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "RaceId",
                table: "Colors");
        }
    }
}
