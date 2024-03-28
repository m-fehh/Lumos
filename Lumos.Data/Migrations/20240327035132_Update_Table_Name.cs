using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lumos.Data.Migrations
{
    public partial class Update_Table_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Tenants_TenantId",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Address_AddressId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Organizations_OrganizationId",
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
                name: "PK_Organizations",
                table: "Organizations");

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
                name: "Organizations",
                newName: "tbOrganizations");

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
                name: "IX_Organizations_TenantId",
                table: "tbOrganizations",
                newName: "IX_tbOrganizations_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbUsers",
                table: "tbUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbTenants",
                table: "tbTenants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbOrganizations",
                table: "tbOrganizations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbAddress",
                table: "tbAddress",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbOrganizations_tbTenants_TenantId",
                table: "tbOrganizations",
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
                name: "FK_tbUsers_tbOrganizations_OrganizationId",
                table: "tbUsers",
                column: "OrganizationId",
                principalTable: "tbOrganizations",
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
                name: "FK_tbOrganizations_tbTenants_TenantId",
                table: "tbOrganizations");

            migrationBuilder.DropForeignKey(
                name: "FK_tbUsers_tbAddress_AddressId",
                table: "tbUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_tbUsers_tbOrganizations_OrganizationId",
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
                name: "PK_tbOrganizations",
                table: "tbOrganizations");

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
                name: "tbOrganizations",
                newName: "Organizations");

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
                name: "IX_tbOrganizations_TenantId",
                table: "Organizations",
                newName: "IX_Organizations_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tenants",
                table: "Tenants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Tenants_TenantId",
                table: "Organizations",
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
                name: "FK_Users_Organizations_OrganizationId",
                table: "Users",
                column: "OrganizationId",
                principalTable: "Organizations",
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
