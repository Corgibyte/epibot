using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Epibot.Migrations
{
    public partial class RemoveReminderSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LoginReminderClients",
                keyColumn: "LoginReminderClientId",
                keyValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "LoginReminderClients",
                columns: new[] { "LoginReminderClientId", "LastAMReminder", "LastPMReminder", "UserId" },
                values: new object[] { 1, new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 184137473578893312ul });
        }
    }
}
