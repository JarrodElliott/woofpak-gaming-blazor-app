using Microsoft.EntityFrameworkCore.Migrations;

namespace WoofpakGamingSiteServerApp.Data.Migrations
{
    public partial class TwitchDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TwitchDescription",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TwitchDescription",
                table: "AspNetUsers");
        }
    }
}
