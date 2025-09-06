using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTaskSubmissionId2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-100",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "61ce6051-01f4-48dd-ab49-7eccc8073c90", "f8fef1f5-6c75-4b63-8591-55d294a3b8bc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-101",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "39f4638e-255c-4e93-93f5-df64fe8ad324", "19fefa9e-9d77-43ea-b73a-d35eb8f77fa6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-102",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4681354a-e710-4e24-8951-4080a73ddb0d", "56ca53e8-c0ba-4567-bf0b-2bc8133c5bff" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-103",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "47967aaf-de73-4901-b362-55036f72164a", "e0b9bcb7-f961-404d-8c19-534cdd69cb57" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-104",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0ce22a48-3dec-473b-aed3-3fbac405542e", "34b09941-f129-44e1-b78a-9ca5e9953589" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-100",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b8734c16-11ee-4526-b291-f776dc86bf11", "d729f4d6-29c7-45c3-a391-f7bbedd2409e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-101",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "473a2da4-51a0-434a-8aa0-ae7cb204d4e5", "471e76b3-e1bc-4b68-abde-359a693490b6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-102",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a87fa382-c255-4fb0-bef0-ea082bfa15ba", "a6cacf26-58ed-4810-967c-ed49c515322e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-103",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "27c4ed33-24f9-4467-b969-3e0c7eded92c", "3cb568f3-015d-43be-ad62-913827a4a08c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-104",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "88b41de9-acdd-4193-91df-bd2cadbb9410", "cbfc80d1-c8b8-4acb-a80a-1d47e1b0bec1" });
        }
    }
}
