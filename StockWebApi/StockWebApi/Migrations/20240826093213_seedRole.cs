using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockWebApi.Migrations
{
    /// <inheritdoc />
    public partial class seedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "62add6ba-5233-4e99-a052-ba554e24b149", null, "Admin", "ADMIN" },
                    { "fef5bf37-bd92-4df6-a56c-2e3fa922ce98", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62add6ba-5233-4e99-a052-ba554e24b149");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fef5bf37-bd92-4df6-a56c-2e3fa922ce98");
        }
    }
}
