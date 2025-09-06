using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTaskSubmissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TaskSubmissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-100",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0f0ecb1d-83df-406f-ba77-f6530f189718", "868c498c-6e24-4aa5-889c-fde6bac449b9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-101",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "21abb1f3-657a-4715-bff5-edb698196ab7", "bf502add-bad4-4b3b-afc7-3b5b83ef6821" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-102",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f67b3c8b-b49f-4865-bf91-57e900a23bb0", "26ac0f2e-4163-42f6-89d6-2e8bdc619a8b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-103",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0a491b38-e9e5-4b2a-a642-780baa8a3ab9", "bf2b9a86-02fb-4724-a544-4244b9f09fee" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-104",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e765bd87-464e-48cb-b2ba-06917c813f21", "dc5fb601-e20e-44a5-a42f-753f8cb18ba9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "TaskSubmissions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-100",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "78f2bb09-e604-4cba-ac5f-46010b4c1def", "a31b6e84-e061-4b8c-a002-9d6c87c5cdfe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-101",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "318ae7e7-9a4b-4135-8a3a-cafc6b44769f", "f556bc4e-f118-404d-a25b-d88b990069ca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-102",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "643dda5b-fab5-4ec6-b85f-2ee6d2261208", "0d427fda-6bf9-4edc-bfe4-4c3c3c359f15" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-103",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4589725e-66e8-415c-a1a9-cafa091f3b79", "2121d994-9448-439d-9af8-1fca2202eb3c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-104",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9f8f7acd-6c82-4641-80af-edd5c08362b5", "0cdb89f5-fd94-4265-8561-0b16c11b9742" });
        }
    }
}
