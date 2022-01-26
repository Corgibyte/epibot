using Microsoft.EntityFrameworkCore.Migrations;

namespace Epibot.Migrations
{
    public partial class BugfixChangeVariableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GitHubTags",
                table: "GitHubTags");

            migrationBuilder.RenameTable(
                name: "GitHubTags",
                newName: "Bylines");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bylines",
                table: "Bylines",
                column: "BylineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Bylines",
                table: "Bylines");

            migrationBuilder.RenameTable(
                name: "Bylines",
                newName: "GitHubTags");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GitHubTags",
                table: "GitHubTags",
                column: "BylineId");
        }
    }
}
