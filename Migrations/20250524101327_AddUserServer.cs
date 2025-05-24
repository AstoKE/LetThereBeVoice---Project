using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LetThereBeVoice.Migrations
{
    /// <inheritdoc />
    public partial class AddUserServer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserServer",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ServerID = table.Column<int>(type: "int", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserServer", x => new { x.UserID, x.ServerID });
                    table.ForeignKey(
                        name: "FK_UserServer_Servers_ServerID",
                        column: x => x.ServerID,
                        principalTable: "Servers",
                        principalColumn: "ServerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserServer_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserServer_ServerID",
                table: "UserServer",
                column: "ServerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserServer");
        }
    }
}
