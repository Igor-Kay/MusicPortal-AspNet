using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicPortal.DAL.Migrations
{
    public partial class addImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99e7516f-6377-4b32-a003-6036790a6641");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc18ec0a-18dc-4468-8cb3-b4f239fde221");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5f51ba38-8aa8-49ef-81ed-f54d6c7f7e9a", "375ce13a-a03b-44cf-9f88-1fc2c93f1d18", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "63fa4b8e-019e-45f7-8059-b0c4e30d0e94", "bd4141f8-1352-4875-b631-86b2bdea7939", "User", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f51ba38-8aa8-49ef-81ed-f54d6c7f7e9a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "63fa4b8e-019e-45f7-8059-b0c4e30d0e94");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dc18ec0a-18dc-4468-8cb3-b4f239fde221", "6b2b7cf1-334c-4f26-8780-57c9b5284c86", "Admin", null });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "99e7516f-6377-4b32-a003-6036790a6641", "d2126dd6-c194-4386-a1e2-670896f92b9e", "User", null });
        }
    }
}
