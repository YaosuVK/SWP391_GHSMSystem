using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessObject.Migrations
{
    /// <inheritdoc />
    public partial class FixDb4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41cad25a-2993-4f50-aa14-e9a9b8de183d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "755bea23-5901-48fb-b055-94dcb5d8a73f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85638ebb-650d-4542-a3b6-aeced8d3cdc9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9224e1c4-d7f2-4c89-b0f3-9a571b29e89e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1d517b7-6642-4822-9cf0-8b7b3476de7b");

            migrationBuilder.AddColumn<int>(
                name: "MaxTestAppointment",
                table: "Slots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "ConsultantPrice",
                table: "ConsultantProfiles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<int>(
                name: "ServicesID",
                table: "AppointmentDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ConsultantProfileID",
                table: "AppointmentDetails",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "46b742b0-c6eb-4a51-b308-b44312b5e6f6", null, "Staff", "STAFF" },
                    { "770c3fd3-bbb2-4f68-b394-d743496ff10a", null, "Manager", "MANAGER" },
                    { "cc2e3255-ca01-41c2-a955-e3716fa01e12", null, "Customer", "CUSTOMER" },
                    { "d3c025c5-a124-4e44-b93c-d4116967afd4", null, "Consultant", "CONSULTANT" },
                    { "f9056437-f720-453f-a050-0890bf1f4670", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentDetails_ConsultantProfileID",
                table: "AppointmentDetails",
                column: "ConsultantProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_AppointmentDetails_ConsultantProfiles_ConsultantProfileID",
                table: "AppointmentDetails",
                column: "ConsultantProfileID",
                principalTable: "ConsultantProfiles",
                principalColumn: "ConsultantProfileID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppointmentDetails_ConsultantProfiles_ConsultantProfileID",
                table: "AppointmentDetails");

            migrationBuilder.DropIndex(
                name: "IX_AppointmentDetails_ConsultantProfileID",
                table: "AppointmentDetails");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46b742b0-c6eb-4a51-b308-b44312b5e6f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "770c3fd3-bbb2-4f68-b394-d743496ff10a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc2e3255-ca01-41c2-a955-e3716fa01e12");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3c025c5-a124-4e44-b93c-d4116967afd4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9056437-f720-453f-a050-0890bf1f4670");

            migrationBuilder.DropColumn(
                name: "MaxTestAppointment",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "ConsultantPrice",
                table: "ConsultantProfiles");

            migrationBuilder.DropColumn(
                name: "ConsultantProfileID",
                table: "AppointmentDetails");

            migrationBuilder.AlterColumn<int>(
                name: "ServicesID",
                table: "AppointmentDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "41cad25a-2993-4f50-aa14-e9a9b8de183d", null, "Admin", "ADMIN" },
                    { "755bea23-5901-48fb-b055-94dcb5d8a73f", null, "Consultant", "CONSULTANT" },
                    { "85638ebb-650d-4542-a3b6-aeced8d3cdc9", null, "Manager", "MANAGER" },
                    { "9224e1c4-d7f2-4c89-b0f3-9a571b29e89e", null, "Staff", "STAFF" },
                    { "c1d517b7-6642-4822-9cf0-8b7b3476de7b", null, "Customer", "CUSTOMER" }
                });
        }
    }
}
