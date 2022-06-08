using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunTrophy.Infrastructure.Migrations
{
    public partial class AlterUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackTimes_Users_ModifyByUserId",
                table: "TrackTimes");

            migrationBuilder.DropIndex(
                name: "IX_TrackTimes_ModifyByUserId",
                table: "TrackTimes");

            migrationBuilder.DropColumn(
                name: "ModifyByUserId",
                table: "TrackTimes");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "ModifyByUserId",
                table: "TrackTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TrackTimes_ModifyByUserId",
                table: "TrackTimes",
                column: "ModifyByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackTimes_Users_ModifyByUserId",
                table: "TrackTimes",
                column: "ModifyByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
