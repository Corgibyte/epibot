using Microsoft.EntityFrameworkCore.Migrations;

namespace Epibot.Migrations
{
    public partial class ChangeBylineVariableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GitHubTagId",
                table: "GitHubTags",
                newName: "BylineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BylineId",
                table: "GitHubTags",
                newName: "GitHubTagId");
        }
    }
}
