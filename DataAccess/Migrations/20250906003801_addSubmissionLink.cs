using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addSubmissionLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubmissionLink",
                table: "TaskSubmissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-100",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "29daab1b-ab39-4ccc-84da-61ef702095cd", "1375776f-b613-416e-85dc-9eeeaaa88667" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-101",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "46a2cb69-9c76-4bfc-91c7-db4528770ecd", "55e4c4fc-6ca7-4be8-b21a-ebf4291d2784" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-102",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a16c57c9-dd51-4049-9b51-329573277565", "02d0c6e7-18c1-4b73-b851-680da7ec8486" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-103",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4dc2f01c-535b-45ff-a33c-fbc5ae8adfb4", "adbd0689-1211-49c8-8dd0-1d9c77d7ca49" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-104",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1263a14f-65f8-4f6a-993e-69ab90502ede", "815d224d-ff15-4f44-bd09-10b1fbef4d6c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmissionLink",
                table: "TaskSubmissions");

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
    }
}
