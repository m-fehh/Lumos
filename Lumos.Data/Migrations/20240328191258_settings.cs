using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lumos.Data.Migrations
{
    public partial class settings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbUsers_tbAddress_AddressId",
                table: "tbUsers");

            migrationBuilder.DropIndex(
                name: "IX_tbUsers_AddressId",
                table: "tbUsers");

            migrationBuilder.RenameColumn(
                name: "ProfileImageUrl",
                table: "tbUsers",
                newName: "ContactMethod");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "tbAddress",
                newName: "Number");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "tbAddress",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbAddress_UserId",
                table: "tbAddress",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_tbAddress_tbUsers_UserId",
                table: "tbAddress",
                column: "UserId",
                principalTable: "tbUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbAddress_tbUsers_UserId",
                table: "tbAddress");

            migrationBuilder.DropIndex(
                name: "IX_tbAddress_UserId",
                table: "tbAddress");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "tbAddress");

            migrationBuilder.RenameColumn(
                name: "ContactMethod",
                table: "tbUsers",
                newName: "ProfileImageUrl");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "tbAddress",
                newName: "Country");

            migrationBuilder.CreateIndex(
                name: "IX_tbUsers_AddressId",
                table: "tbUsers",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbUsers_tbAddress_AddressId",
                table: "tbUsers",
                column: "AddressId",
                principalTable: "tbAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
