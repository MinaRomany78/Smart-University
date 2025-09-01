using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddGeneratedPasswordToApplicationDeletingItFromStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeneratedPassword",
                table: "Students");

            migrationBuilder.AddColumn<string>(
                name: "GeneratedPassword",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-100",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f43d30b6-b811-45c8-854d-52a77ef2d73e", "cb7aed14-9d95-45d5-9780-7d987b6b47f2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-101",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4145be88-c73a-48d7-9fa2-3239b632f4c4", "2cf54af2-3960-437c-954c-547a34e8bd62" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-102",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0c565358-697f-48b0-918c-4054c02f70ff", "9a35de28-f126-4f95-9229-990fd4ab773e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-103",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "16ae69b7-5308-41a7-b355-61aac77e9a7a", "5f78e65f-8c17-463c-9aa5-d51db455966c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-104",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "8be74b2b-e4b9-42a7-b9d6-d71239a952e0", "b55cbbe9-8e7f-4dff-8c3c-59989d92cce2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeneratedPassword",
                table: "Applications");

            migrationBuilder.AddColumn<string>(
                name: "GeneratedPassword",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-100",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "01b27671-9e79-41b7-bad0-dc08e9fa0188", "7acbfc7e-fb12-4851-a5c2-b5cebf0959f0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-101",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "aff4706c-662e-4711-afbd-784eae41ac1f", "6ef5afe8-66e8-4398-ad5c-f8c10574ce78" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-102",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ac04df0a-8f25-45a9-9cb7-9d8d14111f54", "b7fe14d8-31a5-4038-94e8-1abfab001785" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-103",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "34a358c6-0876-4734-aa68-3639212d6275", "6a85f77e-9074-49f0-b6b0-287e24d6fc23" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-104",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "891f02a7-fbcb-46fe-b361-7b3064485d73", "02b49736-62c4-4d7e-8936-f28bba745ce3" });
        }
    }
}
