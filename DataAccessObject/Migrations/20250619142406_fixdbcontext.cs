using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessObject.Migrations
{
    /// <inheritdoc />
    public partial class fixdbcontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultantSlot_AspNetUsers_ConsultantID",
                table: "ConsultantSlot");

            migrationBuilder.DropForeignKey(
                name: "FK_ConsultantSlot_Slots_SlotID",
                table: "ConsultantSlot");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConsultantSlot",
                table: "ConsultantSlot");

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

            migrationBuilder.RenameTable(
                name: "ConsultantSlot",
                newName: "ConsultantSlots");

            migrationBuilder.RenameIndex(
                name: "IX_ConsultantSlot_SlotID",
                table: "ConsultantSlots",
                newName: "IX_ConsultantSlots_SlotID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConsultantSlots",
                table: "ConsultantSlots",
                columns: new[] { "ConsultantID", "SlotID" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "461c5bb0-bd26-421a-bca8-bc9714c511ce", null, "Customer", "CUSTOMER" },
                    { "7396c599-db2d-41bb-a108-13954ae87fb8", null, "Admin", "ADMIN" },
                    { "a0970298-9c81-4516-b4d7-142f70fcbb58", null, "Staff", "STAFF" },
                    { "c1523d27-0905-4f4a-89a2-1b4ebce93f32", null, "Consultant", "CONSULTANT" },
                    { "f3e8b913-68a1-4fd8-a813-0e4ba792a3c8", null, "Manager", "MANAGER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultantSlots_AspNetUsers_ConsultantID",
                table: "ConsultantSlots",
                column: "ConsultantID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultantSlots_Slots_SlotID",
                table: "ConsultantSlots",
                column: "SlotID",
                principalTable: "Slots",
                principalColumn: "SlotID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultantSlots_AspNetUsers_ConsultantID",
                table: "ConsultantSlots");

            migrationBuilder.DropForeignKey(
                name: "FK_ConsultantSlots_Slots_SlotID",
                table: "ConsultantSlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConsultantSlots",
                table: "ConsultantSlots");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "461c5bb0-bd26-421a-bca8-bc9714c511ce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7396c599-db2d-41bb-a108-13954ae87fb8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0970298-9c81-4516-b4d7-142f70fcbb58");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1523d27-0905-4f4a-89a2-1b4ebce93f32");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3e8b913-68a1-4fd8-a813-0e4ba792a3c8");

            migrationBuilder.RenameTable(
                name: "ConsultantSlots",
                newName: "ConsultantSlot");

            migrationBuilder.RenameIndex(
                name: "IX_ConsultantSlots_SlotID",
                table: "ConsultantSlot",
                newName: "IX_ConsultantSlot_SlotID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConsultantSlot",
                table: "ConsultantSlot",
                columns: new[] { "ConsultantID", "SlotID" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultantSlot_AspNetUsers_ConsultantID",
                table: "ConsultantSlot",
                column: "ConsultantID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultantSlot_Slots_SlotID",
                table: "ConsultantSlot",
                column: "SlotID",
                principalTable: "Slots",
                principalColumn: "SlotID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
