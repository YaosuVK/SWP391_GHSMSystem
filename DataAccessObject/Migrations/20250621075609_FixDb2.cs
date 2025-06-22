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
            migrationBuilder.DropIndex(
                name: "IX_CyclePredictions_MenstrualCycleID",
                table: "CyclePredictions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2fb52cad-1684-4113-a512-6a923539157a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41346bb0-cc27-4ff2-807c-ebea941ba8e2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4dd4dc8a-f0c6-4aba-9f14-bb0ebe70007f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4b6e48a-516c-4844-bcc1-f79e87e23216");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0f61d98-2b62-4dee-b95e-f69e0bd4fda7");

            migrationBuilder.CreateIndex(
                name: "IX_CyclePredictions_MenstrualCycleID",
                table: "CyclePredictions",
                column: "MenstrualCycleID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CyclePredictions_MenstrualCycleID",
                table: "CyclePredictions");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2fb52cad-1684-4113-a512-6a923539157a", null, "Customer", "CUSTOMER" },
                    { "41346bb0-cc27-4ff2-807c-ebea941ba8e2", null, "Manager", "MANAGER" },
                    { "4dd4dc8a-f0c6-4aba-9f14-bb0ebe70007f", null, "Consultant", "CONSULTANT" },
                    { "c4b6e48a-516c-4844-bcc1-f79e87e23216", null, "Admin", "ADMIN" },
                    { "f0f61d98-2b62-4dee-b95e-f69e0bd4fda7", null, "Staff", "STAFF" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CyclePredictions_MenstrualCycleID",
                table: "CyclePredictions",
                column: "MenstrualCycleID");
        }
    }
}
