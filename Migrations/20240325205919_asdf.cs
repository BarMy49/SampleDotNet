using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleDotNet.Migrations
{
    public partial class asdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Cummunities_CummunityId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "CummunityUser");

            migrationBuilder.DropTable(
                name: "Cummunities");

            migrationBuilder.RenameColumn(
                name: "CummunityId",
                table: "Posts",
                newName: "CommunityId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_CummunityId",
                table: "Posts",
                newName: "IX_Posts_CommunityId");

            migrationBuilder.CreateTable(
                name: "Communities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommunityUser",
                columns: table => new
                {
                    CommunitiesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityUser", x => new { x.CommunitiesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_CommunityUser_Communities_CommunitiesId",
                        column: x => x.CommunitiesId,
                        principalTable: "Communities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunityUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommunityUser_UsersId",
                table: "CommunityUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Communities_CommunityId",
                table: "Posts",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Communities_CommunityId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "CommunityUser");

            migrationBuilder.DropTable(
                name: "Communities");

            migrationBuilder.RenameColumn(
                name: "CommunityId",
                table: "Posts",
                newName: "CummunityId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_CommunityId",
                table: "Posts",
                newName: "IX_Posts_CummunityId");

            migrationBuilder.CreateTable(
                name: "Cummunities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cummunities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CummunityUser",
                columns: table => new
                {
                    CummunitiesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CummunityUser", x => new { x.CummunitiesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_CummunityUser_Cummunities_CummunitiesId",
                        column: x => x.CummunitiesId,
                        principalTable: "Cummunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CummunityUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CummunityUser_UsersId",
                table: "CummunityUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Cummunities_CummunityId",
                table: "Posts",
                column: "CummunityId",
                principalTable: "Cummunities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
