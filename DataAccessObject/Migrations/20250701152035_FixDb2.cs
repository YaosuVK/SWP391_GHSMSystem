using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessObject.Migrations
{
    /// <inheritdoc />
    public partial class FixDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5281f337-ded0-4e06-bbe8-6b7b5a24653a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64b8f6e8-0dd1-4760-9a6f-fddd952e119c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76d262fa-46d2-4768-9c39-7762bb228e1a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e7139a6-2464-43a2-998e-92e7d29dd346");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9dfd03b5-94b8-49d6-994d-db3b8a799b2b");

            migrationBuilder.AddColumn<string>(
                name: "AppointmentCode",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredTime",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "AppointmentCode",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ExpiredTime",
                table: "Appointments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5281f337-ded0-4e06-bbe8-6b7b5a24653a", null, "Consultant", "CONSULTANT" },
                    { "64b8f6e8-0dd1-4760-9a6f-fddd952e119c", null, "Admin", "ADMIN" },
                    { "76d262fa-46d2-4768-9c39-7762bb228e1a", null, "Customer", "CUSTOMER" },
                    { "7e7139a6-2464-43a2-998e-92e7d29dd346", null, "Manager", "MANAGER" },
                    { "9dfd03b5-94b8-49d6-994d-db3b8a799b2b", null, "Staff", "STAFF" }
                });
        }
    }
}
