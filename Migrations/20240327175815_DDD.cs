using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleDotNet.Migrations
{
    public partial class DDD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RedditKarma",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "GoonRatio",
                table: "Posts",
                newName: "Gratio");

            migrationBuilder.AddColumn<int>(
                name: "Garma",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Garma",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Gratio",
                table: "Posts",
                newName: "GoonRatio");

            migrationBuilder.AddColumn<int>(
                name: "RedditKarma",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
