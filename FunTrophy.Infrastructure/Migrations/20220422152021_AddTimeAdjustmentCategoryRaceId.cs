using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunTrophy.Infrastructure.Migrations
{
    public partial class AddTimeAdjustmentCategoryRaceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RaceId",
                table: "TimeAdjustmentCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TimeAdjustmentCategories_RaceId",
                table: "TimeAdjustmentCategories",
                column: "RaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeAdjustmentCategories_Races_RaceId",
                table: "TimeAdjustmentCategories",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeAdjustmentCategories_Races_RaceId",
                table: "TimeAdjustmentCategories");

            migrationBuilder.DropIndex(
                name: "IX_TimeAdjustmentCategories_RaceId",
                table: "TimeAdjustmentCategories");

            migrationBuilder.DropColumn(
                name: "RaceId",
                table: "TimeAdjustmentCategories");
        }
    }
}
