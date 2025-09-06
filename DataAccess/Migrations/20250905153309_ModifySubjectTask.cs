using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModifySubjectTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTasks_Doctors_DoctorId",
                table: "SubjectTasks");

            migrationBuilder.DropColumn(
                name: "AssignedBy",
                table: "SubjectTasks");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "SubjectTasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AssistantId",
                table: "SubjectTasks",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTasks_AssistantId",
                table: "SubjectTasks",
                column: "AssistantId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTasks_Assistants_AssistantId",
                table: "SubjectTasks",
                column: "AssistantId",
                principalTable: "Assistants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTasks_Doctors_DoctorId",
                table: "SubjectTasks",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTasks_Assistants_AssistantId",
                table: "SubjectTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTasks_Doctors_DoctorId",
                table: "SubjectTasks");

            migrationBuilder.DropIndex(
                name: "IX_SubjectTasks_AssistantId",
                table: "SubjectTasks");

            migrationBuilder.DropColumn(
                name: "AssistantId",
                table: "SubjectTasks");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "SubjectTasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignedBy",
                table: "SubjectTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-100",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6c3ae037-271b-4b8f-b604-1099c6181e71", "f7814cb3-d35d-4d9e-af43-20dfc4c6bba4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-101",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5fcdd3e8-8a8d-4560-80dc-a0ea3bd845a3", "4b75a0ae-0b79-4aa9-8cdf-28a231c81d00" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-102",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "72ba83fa-2663-442d-ab9f-ecb7ad794f9c", "8de2f2b5-cbe6-4b14-9518-25bf34b40179" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-103",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "08bc0a6d-3786-42d1-a2a3-8b9977c6b472", "416d1d70-e851-4422-a292-c9e97f0757d0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "inst-user-104",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "1f7be248-1cac-413b-809b-40cc616645f8", "2a6e8edf-ea85-4e86-ab93-56bc0467e813" });

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTasks_Doctors_DoctorId",
                table: "SubjectTasks",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
