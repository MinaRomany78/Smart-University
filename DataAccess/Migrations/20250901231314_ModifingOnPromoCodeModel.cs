using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModifingOnPromoCodeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentUsage",
                table: "PromoCodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxUsage",
                table: "PromoCodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-100",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "832427f9-45b6-4f31-bb37-4d8fdd229132", "dcba3c03-1e89-4d50-8b41-9e502dfa79a7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-101",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "e68f7b75-1ee1-4828-aaf9-aa8d21637e3d", "9c892227-b354-44ed-a22f-2b9f8a08e8a9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-102",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "569e2f11-e862-4445-b25f-5bf1553cf383", "22a8432c-5248-467d-8e6d-e5203c8d9817" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-103",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cfa98fb7-9f47-419c-a2e1-6e8d82b7106e", "f0d3d7e3-e1c4-406f-aae9-c876d14a0447" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-104",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6c7c419d-a30a-4402-97d4-087d8d9720d1", "49087075-b405-439f-b3ef-6c283e3d9199" });

            migrationBuilder.UpdateData(
                table: "PromoCodes",
                keyColumn: "Code",
                keyValue: "PROMO10",
                columns: new[] { "CurrentUsage", "MaxUsage" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "PromoCodes",
                keyColumn: "Code",
                keyValue: "STUDENT20",
                columns: new[] { "CurrentUsage", "MaxUsage" },
                values: new object[] { 0, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentUsage",
                table: "PromoCodes");

            migrationBuilder.DropColumn(
                name: "MaxUsage",
                table: "PromoCodes");

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
        }
    }
}
