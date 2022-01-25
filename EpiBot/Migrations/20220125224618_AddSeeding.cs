using Microsoft.EntityFrameworkCore.Migrations;

namespace Epibot.Migrations
{
    public partial class AddSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GitHubTags",
                columns: new[] { "GitHubTagId", "Email", "Name" },
                values: new object[] { 1, "hannah@corgibyte.com", "Hannah Young" });

            migrationBuilder.InsertData(
                table: "GitHubTags",
                columns: new[] { "GitHubTagId", "Email", "Name" },
                values: new object[] { 2, "abminnick@gmail.com", "Aaron Minnick" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GitHubTags",
                keyColumn: "GitHubTagId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GitHubTags",
                keyColumn: "GitHubTagId",
                keyValue: 2);
        }
    }
}
