using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LetThereBeVoice.Migrations
{
    /// <inheritdoc />
    public partial class AddVoiceSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VoiceSessions",
                columns: table => new
                {
                    VoiceSessionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ChannelID = table.Column<int>(type: "int", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoiceSessions", x => x.VoiceSessionID);
                    table.ForeignKey(
                        name: "FK_VoiceSessions_Channels_ChannelID",
                        column: x => x.ChannelID,
                        principalTable: "Channels",
                        principalColumn: "ChannelID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoiceSessions_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoiceSessions_ChannelID",
                table: "VoiceSessions",
                column: "ChannelID");

            migrationBuilder.CreateIndex(
                name: "IX_VoiceSessions_UserID",
                table: "VoiceSessions",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoiceSessions");
        }
    }
}
