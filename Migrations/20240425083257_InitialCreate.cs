using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleDotNet.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Gommunities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuserCount = table.Column<int>(type: "int", nullable: false),
                    PostCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gommunities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GommunityGuser",
                columns: table => new
                {
                    GommunitiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GusersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GommunityGuser", x => new { x.GommunitiesId, x.GusersId });
                    table.ForeignKey(
                        name: "FK_GommunityGuser_AspNetUsers_GusersId",
                        column: x => x.GusersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GommunityGuser_Gommunities_GommunitiesId",
                        column: x => x.GommunitiesId,
                        principalTable: "Gommunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gratio = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    GuserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GommunityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_GuserId",
                        column: x => x.GuserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Gommunities_GommunityId",
                        column: x => x.GommunityId,
                        principalTable: "Gommunities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GommunityGuser_GusersId",
                table: "GommunityGuser",
                column: "GusersId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_GommunityId",
                table: "Posts",
                column: "GommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_GuserId",
                table: "Posts",
                column: "GuserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "GommunityGuser");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Gommunities");
        }
    }
}