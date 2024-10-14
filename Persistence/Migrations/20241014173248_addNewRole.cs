using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addNewRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { "bfff56e8-8785-4c78-907b-7d22254556f4", null, "", "User", "User" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6f41b021-3b66-484f-a673-7596f9c1aa07",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "85ce7bf8-d6c9-42a7-b644-e612572ba4ae", "AQAAAAIAAYagAAAAEEJxxHOm35jp57Dsbw93SWaT1FtUg1Pkcv14X/jxmxtPKmoiNj2hGoqDLFkZ0Df7nQ==", "872f47d9-e1b4-4e16-9e0a-0f909af4268a", "Root" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bfff56e8-8785-4c78-907b-7d22254556f4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6f41b021-3b66-484f-a673-7596f9c1aa07",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "dbd8a24a-cfbf-42ac-b0ab-0a464dbe4dc2", "AQAAAAIAAYagAAAAELoNTINkSRn1/VjcXPbmHB9ok1iWYsu45WliJFtn2EQpRtWdCkdnaLGfGHsQQNsFvg==", "bb20e3a1-aad0-4741-ba3c-4df9f3dde267", "root" });
        }
    }
}
