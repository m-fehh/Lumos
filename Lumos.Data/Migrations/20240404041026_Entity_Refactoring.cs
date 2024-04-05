using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lumos.Data.Migrations
{
    public partial class Entity_Refactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitsUsers");

            migrationBuilder.DropTable(
                name: "tbAddress");

            migrationBuilder.DropTable(
                name: "tbUnits");

            migrationBuilder.DropColumn(
                name: "AccessLevel",
                table: "tbUsers");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "tbUsers");

            migrationBuilder.DropColumn(
                name: "ContactMethod",
                table: "tbUsers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "tbUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "tbUsers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "tbUsers");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "tbUsers");

            migrationBuilder.DropColumn(
                name: "Branch",
                table: "tbTenants");

            migrationBuilder.DropColumn(
                name: "Uf",
                table: "tbTenants");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "tbUsers",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "tbTenants",
                newName: "State");

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "tbUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(14)",
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tbTenants",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "tbTenants",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "tbTenants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "tbUnits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CpfCnpj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbUnits_tbTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "tbTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserUnits",
                columns: table => new
                {
                    UnitsId = table.Column<long>(type: "bigint", nullable: false),
                    UsersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUnits", x => new { x.UnitsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserUnits_tbUnits_UnitsId",
                        column: x => x.UnitsId,
                        principalTable: "tbUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUnits_tbUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "tbUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbUnits_TenantId",
                table: "tbUnits",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserUnits_UsersId",
                table: "UserUnits",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserUnits");

            migrationBuilder.DropTable(
                name: "tbUnits");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "tbTenants");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "tbUsers",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "tbTenants",
                newName: "Description");

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "tbUsers",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "AccessLevel",
                table: "tbUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "AddressId",
                table: "tbUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ContactMethod",
                table: "tbUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "tbUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "tbUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "tbUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "tbUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tbTenants",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "tbTenants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Branch",
                table: "tbTenants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uf",
                table: "tbTenants",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "tbAddress",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbAddress_tbUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "tbUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbUnits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    CpfCnpj = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbUnits_tbTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "tbTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_tbAddress_UserId",
                table: "tbAddress",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbUnits_TenantId",
                table: "tbUnits",
                column: "TenantId");
        }
    }
}
