using Microsoft.EntityFrameworkCore.Migrations;

namespace AMP.Persistence.Migrations
{
    public partial class Entitystatustypechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Users",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Normal");

            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Services",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Normal");

            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Requests",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Normal");

            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Ratings",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Normal");

            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Payments",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Normal");

            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Orders",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Normal");

            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Languages",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Normal");

            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Images",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Normal");

            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Disputes",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Normal");

            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Customers",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Normal");

            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Artisans",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Normal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36,
                oldDefaultValue: "Normal");

            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36,
                oldDefaultValue: "Normal");

            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36,
                oldDefaultValue: "Normal");

            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Ratings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36,
                oldDefaultValue: "Normal");

            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36,
                oldDefaultValue: "Normal");

            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36,
                oldDefaultValue: "Normal");

            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Languages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36,
                oldDefaultValue: "Normal");

            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36,
                oldDefaultValue: "Normal");

            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Disputes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36,
                oldDefaultValue: "Normal");

            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36,
                oldDefaultValue: "Normal");

            migrationBuilder.AlterColumn<string>(
                name: "EntityStatus",
                table: "Artisans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Normal",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36,
                oldDefaultValue: "Normal");
        }
    }
}
