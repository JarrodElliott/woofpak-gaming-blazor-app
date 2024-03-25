using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WoofpakGamingSiteServerApp.Data.Migrations
{
    public partial class ExtraLifeDonations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExtraLifeEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraLifeEvent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExtraLifeTeam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExtraLifeEventId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraLifeTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraLifeTeam_ExtraLifeEvent_ExtraLifeEventId",
                        column: x => x.ExtraLifeEventId,
                        principalTable: "ExtraLifeEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExtraLifeParticipant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumDonations = table.Column<int>(type: "int", nullable: false),
                    SumDonations = table.Column<double>(type: "float", nullable: false),
                    IsStreamLive = table.Column<bool>(type: "bit", nullable: false),
                    FundraisingGoal = table.Column<double>(type: "float", nullable: false),
                    AvatarImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDateUTC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExtraLifeEventId = table.Column<int>(type: "int", nullable: true),
                    ExtraLifeTeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraLifeParticipant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraLifeParticipant_ExtraLifeEvent_ExtraLifeEventId",
                        column: x => x.ExtraLifeEventId,
                        principalTable: "ExtraLifeEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtraLifeParticipant_ExtraLifeTeam_ExtraLifeTeamId",
                        column: x => x.ExtraLifeTeamId,
                        principalTable: "ExtraLifeTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExtraLifeDonation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonationAmount = table.Column<double>(type: "float", nullable: false),
                    DontationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExtraLifeTeamId = table.Column<int>(type: "int", nullable: true),
                    ExtraLifeEventId = table.Column<int>(type: "int", nullable: true),
                    ExtraLifeParticipantId = table.Column<int>(type: "int", nullable: true),
                    WoofpakKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraLifeDonation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraLifeDonation_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExtraLifeDonation_ExtraLifeEvent_ExtraLifeEventId",
                        column: x => x.ExtraLifeEventId,
                        principalTable: "ExtraLifeEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtraLifeDonation_ExtraLifeParticipant_ExtraLifeParticipantId",
                        column: x => x.ExtraLifeParticipantId,
                        principalTable: "ExtraLifeParticipant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtraLifeDonation_ExtraLifeTeam_ExtraLifeTeamId",
                        column: x => x.ExtraLifeTeamId,
                        principalTable: "ExtraLifeTeam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtraLifeDonation_ExtraLifeEventId",
                table: "ExtraLifeDonation",
                column: "ExtraLifeEventId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraLifeDonation_ExtraLifeParticipantId",
                table: "ExtraLifeDonation",
                column: "ExtraLifeParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraLifeDonation_ExtraLifeTeamId",
                table: "ExtraLifeDonation",
                column: "ExtraLifeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraLifeDonation_UserId",
                table: "ExtraLifeDonation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraLifeParticipant_ExtraLifeEventId",
                table: "ExtraLifeParticipant",
                column: "ExtraLifeEventId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraLifeParticipant_ExtraLifeTeamId",
                table: "ExtraLifeParticipant",
                column: "ExtraLifeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraLifeTeam_ExtraLifeEventId",
                table: "ExtraLifeTeam",
                column: "ExtraLifeEventId");

            migrationBuilder.Sql(@"INSERT INTO ExtraLifeEvent (Id, Name)
                                    VALUES(550, 'Extra Life 2021')");

            migrationBuilder.Sql(@"INSERT INTO ExtraLifeTeam (Id, Name, ExtraLifeEventId)
                                    VALUES(56545, 'Woofpak', 550)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtraLifeDonation");

            migrationBuilder.DropTable(
                name: "ExtraLifeParticipant");

            migrationBuilder.DropTable(
                name: "ExtraLifeTeam");

            migrationBuilder.DropTable(
                name: "ExtraLifeEvent");
        }
    }
}
