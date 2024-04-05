using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lumos.Data.Migrations
{
    public partial class Update_Table_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Tenants_TenantId",
                table: "Units");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Address_AddressId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Units_OrganizationId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Tenants_TenantId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Units",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "tbUsers");

            migrationBuilder.RenameTable(
                name: "Tenants",
                newName: "tbTenants");

            migrationBuilder.RenameTable(
                name: "Units",
                newName: "tbUnits");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "tbAddress");

            migrationBuilder.RenameIndex(
                name: "IX_Users_TenantId",
                table: "tbUsers",
                newName: "IX_tbUsers_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_OrganizationId",
                table: "tbUsers",
                newName: "IX_tbUsers_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_AddressId",
                table: "tbUsers",
                newName: "IX_tbUsers_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Units_TenantId",
                table: "tbUnits",
                newName: "IX_tbUnits_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbUsers",
                table: "tbUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbTenants",
                table: "tbTenants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbUnits",
                table: "tbUnits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbAddress",
                table: "tbAddress",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbUnits_tbTenants_TenantId",
                table: "tbUnits",
                column: "TenantId",
                principalTable: "tbTenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbUsers_tbAddress_AddressId",
                table: "tbUsers",
                column: "AddressId",
                principalTable: "tbAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbUsers_tbUnits_OrganizationId",
                table: "tbUsers",
                column: "OrganizationId",
                principalTable: "tbUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tbUsers_tbTenants_TenantId",
                table: "tbUsers",
                column: "TenantId",
                principalTable: "tbTenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbUnits_tbTenants_TenantId",
                table: "tbUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_tbUsers_tbAddress_AddressId",
                table: "tbUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_tbUsers_tbUnits_OrganizationId",
                table: "tbUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_tbUsers_tbTenants_TenantId",
                table: "tbUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbUsers",
                table: "tbUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbTenants",
                table: "tbTenants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbUnits",
                table: "tbUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tbAddress",
                table: "tbAddress");

            migrationBuilder.RenameTable(
                name: "tbUsers",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "tbTenants",
                newName: "Tenants");

            migrationBuilder.RenameTable(
                name: "tbUnits",
                newName: "Units");

            migrationBuilder.RenameTable(
                name: "tbAddress",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_tbUsers_TenantId",
                table: "Users",
                newName: "IX_Users_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_tbUsers_OrganizationId",
                table: "Users",
                newName: "IX_Users_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_tbUsers_AddressId",
                table: "Users",
                newName: "IX_Users_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_tbUnits_TenantId",
                table: "Units",
                newName: "IX_Units_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Units",
                table: "Units",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Tenants_TenantId",
                table: "Units",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Address_AddressId",
                table: "Users",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Units_OrganizationId",
                table: "Users",
                column: "OrganizationId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Tenants_TenantId",
                table: "Users",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
