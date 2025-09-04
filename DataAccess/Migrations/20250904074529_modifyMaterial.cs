using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class modifyMaterial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MaterialType",
                table: "Materials",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AssistantId",
                table: "Materials",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Materials",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-100",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ef8a219e-4b73-4172-86d9-c0a1312cf8d3", "0b3e32b3-aa1a-4684-97c5-18b9a9ce86b5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-101",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "12a527d1-f4fc-4167-8b36-f1d06088368f", "e3bcb0b1-3782-45f3-8b8d-8c97f10f2987" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-102",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "456572f5-2990-4e17-875f-b15bcdf9815b", "4e8a0b4c-0dde-43bc-bbd5-da739f7f9e2f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-103",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ea1c4a17-48fa-4786-b343-abf76d375743", "5506107f-aec4-45e7-8953-debf359b1e13" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-104",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ff2de54a-a487-403f-b337-72072881be9f", "dc9da7c5-7372-44f1-8120-e0d4c697c92e" });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_AssistantId",
                table: "Materials",
                column: "AssistantId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_DoctorId",
                table: "Materials",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Assistants_AssistantId",
                table: "Materials",
                column: "AssistantId",
                principalTable: "Assistants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Doctors_DoctorId",
                table: "Materials",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Assistants_AssistantId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Doctors_DoctorId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_AssistantId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_DoctorId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "AssistantId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Materials");

            migrationBuilder.AlterColumn<string>(
                name: "MaterialType",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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
        }
    }
}
