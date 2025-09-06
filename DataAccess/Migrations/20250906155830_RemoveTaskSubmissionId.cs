using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTaskSubmissionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskSubmissionId",
                table: "Feedbacks",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_TaskSubmissionId",
                table: "Feedbacks",
                column: "TaskSubmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_TaskSubmissions_TaskSubmissionId",
                table: "Feedbacks",
                column: "TaskSubmissionId",
                principalTable: "TaskSubmissions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_TaskSubmissions_TaskSubmissionId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_TaskSubmissionId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "TaskSubmissionId",
                table: "Feedbacks");

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
    }
}
