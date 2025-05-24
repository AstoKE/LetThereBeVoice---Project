using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LetThereBeVoice.Migrations
{
    /// <inheritdoc />
    public partial class AddLastActivityToChannel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastActivity",
                table: "Channels",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastActivity",
                table: "Channels");
        }
    }
}
