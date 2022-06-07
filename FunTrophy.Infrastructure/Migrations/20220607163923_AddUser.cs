using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FunTrophy.Infrastructure.Migrations
{
    public partial class AddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModifyByUserId",
                table: "TrackTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackTimes_Users_ModifyByUserId",
                table: "TrackTimes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_TrackTimes_ModifyByUserId",
                table: "TrackTimes");

            migrationBuilder.DropColumn(
                name: "ModifyByUserId",
                table: "TrackTimes");
        }
    }
}
