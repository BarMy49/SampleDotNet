using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleDotNet.Migrations
{
    public partial class EdycjaTabeliPost_DodaniCumminityId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Cummunities_CummunityId",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "CummunityId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Cummunities_CummunityId",
                table: "Posts",
                column: "CummunityId",
                principalTable: "Cummunities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Cummunities_CummunityId",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "CummunityId",
                table: "Posts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Cummunities_CummunityId",
                table: "Posts",
                column: "CummunityId",
                principalTable: "Cummunities",
                principalColumn: "Id");
        }
    }
}
