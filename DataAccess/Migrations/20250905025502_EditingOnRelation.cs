using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditingOnRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-100",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b6e337de-ea0e-473e-8325-68ffc897a17e", "2f7385b1-5301-4ecc-93e1-daf17a05aba6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-101",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "472ab224-9505-49bf-811f-785eb1bbd12b", "edd77260-7bc9-4f60-bc65-ae0b971b1317" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-102",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5fa99e15-3411-493e-b017-d35ec35e0492", "2436a085-16e9-49b2-a8f2-25c0103ac735" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-103",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "c61a1d47-46a4-4511-9d7a-6ec039f3ea21", "7c90cdc6-dabf-4ebb-aa5e-a44750ea03a2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-104",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "830f2170-5584-465c-b6ba-febd9d29ae8c", "2abc7b44-7719-475b-9866-a7c02b30ab7d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-100",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "47b3ebbb-4ec7-4fe3-bb02-83b10fd6362f", "379eb333-1317-42a8-b1a7-e68040efa60b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-101",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7baeb829-7087-423d-bab9-225b2e35e1b4", "ec6fd738-d9a5-4732-b6c6-6a39e04254ef" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-102",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "015fa604-76cc-4df2-9848-a44769059da1", "add1582c-6ae8-472f-b5b4-055c778489b7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-103",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "50e48d99-c3ea-498c-a4b4-294bc44e4d1b", "ba5e27d7-474d-4817-9b5e-d022a677d812" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-104",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "07b79db5-8bc5-4313-a544-756e5bf41a3d", "1c20dc16-f595-4277-ae61-63537358b565" });
        }
    }
}
