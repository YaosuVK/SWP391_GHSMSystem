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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
