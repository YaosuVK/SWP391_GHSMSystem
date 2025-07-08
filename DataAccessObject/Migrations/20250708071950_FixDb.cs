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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
