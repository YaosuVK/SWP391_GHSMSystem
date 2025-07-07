using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessObject.Migrations
{
    /// <inheritdoc />
    public partial class FixDb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cfaf195-ad71-43b0-bd26-ec914f8cc4db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a54f8c5-09f5-4d1d-84ef-24b03d32a321");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95ac6540-1eeb-4dc2-921e-d3ca364aa603");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfa5e049-259d-468f-9f89-e67237248990");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e543a6c7-eb57-4e89-aec7-9504f19ab418");

            migrationBuilder.DropColumn(
                name: "AppointmentType",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "ServiceType",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "ConsultationFee",
                table: "Appointments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "STIsTestFee",
                table: "Appointments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "remainingBalance",
                table: "Appointments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "058a70cb-c699-4917-b786-387a92111ac6", null, "Manager", "MANAGER" },
                    { "835809ba-c12c-48f0-b652-bf626bbd3c33", null, "Consultant", "CONSULTANT" },
                    { "8917dcef-1ad4-4042-8ae2-72bf6932691c", null, "Customer", "CUSTOMER" },
                    { "d49b884a-6b76-4836-ab55-5b4ce947fe2d", null, "Staff", "STAFF" },
                    { "df9eba5a-26e4-4617-a6dc-e24c0ecd6d2b", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "058a70cb-c699-4917-b786-387a92111ac6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "835809ba-c12c-48f0-b652-bf626bbd3c33");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8917dcef-1ad4-4042-8ae2-72bf6932691c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d49b884a-6b76-4836-ab55-5b4ce947fe2d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df9eba5a-26e4-4617-a6dc-e24c0ecd6d2b");

            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ConsultationFee",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "STIsTestFee",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "remainingBalance",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentType",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3cfaf195-ad71-43b0-bd26-ec914f8cc4db", null, "Consultant", "CONSULTANT" },
                    { "8a54f8c5-09f5-4d1d-84ef-24b03d32a321", null, "Manager", "MANAGER" },
                    { "95ac6540-1eeb-4dc2-921e-d3ca364aa603", null, "Staff", "STAFF" },
                    { "dfa5e049-259d-468f-9f89-e67237248990", null, "Customer", "CUSTOMER" },
                    { "e543a6c7-eb57-4e89-aec7-9504f19ab418", null, "Admin", "ADMIN" }
                });
        }
    }
}
