using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lumos.Data.Migrations
{
    public partial class Adjust_Table_Relationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbUsers_tbUnits_OrganizationId",
                table: "tbUsers");

            migrationBuilder.DropIndex(
                name: "IX_tbUsers_OrganizationId",
                table: "tbUsers");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbUsers");

            migrationBuilder.CreateTable(
                name: "UnitsUsers",
                columns: table => new
                {
                    UnitsId = table.Column<long>(type: "bigint", nullable: false),
                    UsersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitsUsers", x => new { x.UnitsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UnitsUsers_tbUnits_UnitsId",
                        column: x => x.UnitsId,
                        principalTable: "tbUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitsUsers_tbUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "tbUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitsUsers_UsersId",
                table: "UnitsUsers",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitsUsers");

            migrationBuilder.AddColumn<long>(
                name: "OrganizationId",
                table: "tbUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_tbUsers_OrganizationId",
                table: "tbUsers",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbUsers_tbUnits_OrganizationId",
                table: "tbUsers",
                column: "OrganizationId",
                principalTable: "tbUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
