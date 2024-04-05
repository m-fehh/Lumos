using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lumos.Data.Migrations
{
    public partial class New_Adjusted_Columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cnpj",
                table: "tbUnits");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "tbUnits",
                newName: "Level");

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "tbUsers",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AddColumn<string>(
                name: "AccessLevel",
                table: "tbUsers",
                type: "nvarchar(max)",
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

            migrationBuilder.AddColumn<string>(
                name: "Branch",
                table: "tbTenants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "tbTenants",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "tbTenants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uf",
                table: "tbTenants",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tbUnits",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CpfCnpj",
                table: "tbUnits",
                type: "nvarchar(18)",
                maxLength: 18,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessLevel",
                table: "tbUsers");

            migrationBuilder.DropColumn(
                name: "Branch",
                table: "tbTenants");

            migrationBuilder.DropColumn(
                name: "City",
                table: "tbTenants");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "tbTenants");

            migrationBuilder.DropColumn(
                name: "Uf",
                table: "tbTenants");

            migrationBuilder.DropColumn(
                name: "CpfCnpj",
                table: "tbUnits");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "tbUnits",
                newName: "Description");

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "tbUsers",
                type: "nvarchar(11)",
                maxLength: 11,
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
                name: "Name",
                table: "tbUnits",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Cnpj",
                table: "tbUnits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
