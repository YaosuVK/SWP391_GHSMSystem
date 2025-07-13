using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessObject.Migrations
{
    /// <inheritdoc />
    public partial class FixDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41c80cd8-ed5b-4383-a054-aeb0242b9167");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "825c9716-10a9-4a86-a452-ebbc9cc6fb1c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "847ff751-c02f-44f1-88ff-8a205351cb91");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2a2a2cb-4189-4428-86c5-2db7f826f3e7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e08189d3-2c63-4b42-8848-c7cbb8552cbd");

            migrationBuilder.AddColumn<string>(
                name: "receiverName",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0cf05af2-7c35-4e89-833c-43cb9ac5be3c", null, "Consultant", "CONSULTANT" },
                    { "2517234f-94c2-45c8-8bc1-b0d78daa3685", null, "Staff", "STAFF" },
                    { "95545edf-9253-4c27-9381-825326609c9b", null, "Admin", "ADMIN" },
                    { "da1b1329-931b-4325-9948-5f595a632953", null, "Manager", "MANAGER" },
                    { "e2d78ba8-484d-4cd2-a6c7-d1729d80544a", null, "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0cf05af2-7c35-4e89-833c-43cb9ac5be3c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2517234f-94c2-45c8-8bc1-b0d78daa3685");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95545edf-9253-4c27-9381-825326609c9b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da1b1329-931b-4325-9948-5f595a632953");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2d78ba8-484d-4cd2-a6c7-d1729d80544a");

            migrationBuilder.DropColumn(
                name: "receiverName",
                table: "Messages");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "41c80cd8-ed5b-4383-a054-aeb0242b9167", null, "Admin", "ADMIN" },
                    { "825c9716-10a9-4a86-a452-ebbc9cc6fb1c", null, "Staff", "STAFF" },
                    { "847ff751-c02f-44f1-88ff-8a205351cb91", null, "Customer", "CUSTOMER" },
                    { "a2a2a2cb-4189-4428-86c5-2db7f826f3e7", null, "Consultant", "CONSULTANT" },
                    { "e08189d3-2c63-4b42-8848-c7cbb8552cbd", null, "Manager", "MANAGER" }
                });
        }
    }
}
