using Microsoft.EntityFrameworkCore.Migrations;

namespace WoofpakGamingSiteServerApp.Data.Migrations
{
    public partial class ExtraLifeStreamerBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsExtraLifeStreamer",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitchUsername",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExtraLifeStreamer",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TwitchUsername",
                table: "AspNetUsers");
        }
    }
}
