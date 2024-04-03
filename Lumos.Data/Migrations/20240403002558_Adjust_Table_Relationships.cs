using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lumos.Data.Migrations
{
    public partial class Adjust_Table_Relationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbUsers_tbOrganizations_OrganizationId",
                table: "tbUsers");

            migrationBuilder.DropIndex(
                name: "IX_tbUsers_OrganizationId",
                table: "tbUsers");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "tbUsers");

            migrationBuilder.CreateTable(
                name: "OrganizationsUsers",
                columns: table => new
                {
                    OrganizationsId = table.Column<long>(type: "bigint", nullable: false),
                    UsersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationsUsers", x => new { x.OrganizationsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_OrganizationsUsers_tbOrganizations_OrganizationsId",
                        column: x => x.OrganizationsId,
                        principalTable: "tbOrganizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationsUsers_tbUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "tbUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationsUsers_UsersId",
                table: "OrganizationsUsers",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationsUsers");

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
                name: "FK_tbUsers_tbOrganizations_OrganizationId",
                table: "tbUsers",
                column: "OrganizationId",
                principalTable: "tbOrganizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
