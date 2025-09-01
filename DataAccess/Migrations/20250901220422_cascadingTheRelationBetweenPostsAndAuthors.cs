using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class cascadingTheRelationBetweenPostsAndAuthors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunityPosts_AspNetUsers_AuthorId",
                table: "CommunityPosts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-100",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "463c7195-8acc-4eb8-a1c3-29f3ff2aade2", "a88ee851-ede0-4e79-aad3-dd7d0cf6df19" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-101",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "972f6515-fb8a-45fb-a6f3-68dbbc5fbd38", "18a5a29e-bce8-4b52-b6bf-193b9c2c6f09" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-102",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a4a320f3-bed4-4b46-8067-92b8df13ebf5", "c2f9cee4-8f29-439b-99b3-5890cda07fa1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-103",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5431a567-f727-4997-9d6e-424e61152ab6", "6e1f1919-d210-415a-9263-71e5c99b348b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-104",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "715d6520-6b07-46f9-90e2-608f145465f4", "4bf565af-a1d8-4fa2-ab66-72f7f4a9603d" });

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityPosts_AspNetUsers_AuthorId",
                table: "CommunityPosts",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunityPosts_AspNetUsers_AuthorId",
                table: "CommunityPosts");

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

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityPosts_AspNetUsers_AuthorId",
                table: "CommunityPosts",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
