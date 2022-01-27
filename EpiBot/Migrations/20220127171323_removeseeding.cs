using Microsoft.EntityFrameworkCore.Migrations;

namespace Epibot.Migrations
{
    public partial class removeseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bylines",
                keyColumn: "BylineId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bylines",
                keyColumn: "BylineId",
                keyValue: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bylines",
                columns: new[] { "BylineId", "Email", "Name" },
                values: new object[] { 1, "hannah@corgibyte.com", "Hannah Young" });

            migrationBuilder.InsertData(
                table: "Bylines",
                columns: new[] { "BylineId", "Email", "Name" },
                values: new object[] { 2, "abminnick@gmail.com", "Aaron Minnick" });
        }
    }
}
