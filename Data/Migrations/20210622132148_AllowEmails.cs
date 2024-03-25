using Microsoft.EntityFrameworkCore.Migrations;

namespace WoofpakGamingSiteServerApp.Data.Migrations
{
    public partial class AllowEmails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowEmails",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowEmails",
                table: "AspNetUsers");
        }
    }
}
