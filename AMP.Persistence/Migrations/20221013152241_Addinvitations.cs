using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMP.Persistence.Migrations
{
    public partial class Addinvitations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 810, DateTimeKind.Utc).AddTicks(3346),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 943, DateTimeKind.Utc).AddTicks(768));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 810, DateTimeKind.Utc).AddTicks(179),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 942, DateTimeKind.Utc).AddTicks(6242));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 805, DateTimeKind.Utc).AddTicks(1088),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 942, DateTimeKind.Utc).AddTicks(1682));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Registrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 804, DateTimeKind.Utc).AddTicks(1200),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 936, DateTimeKind.Utc).AddTicks(9856));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 803, DateTimeKind.Utc).AddTicks(5913),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 935, DateTimeKind.Utc).AddTicks(8425));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 802, DateTimeKind.Utc).AddTicks(3523),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 935, DateTimeKind.Utc).AddTicks(1325));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 793, DateTimeKind.Utc).AddTicks(8026),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 919, DateTimeKind.Utc).AddTicks(1188));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Languages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 793, DateTimeKind.Utc).AddTicks(4748),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 918, DateTimeKind.Utc).AddTicks(6998));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 792, DateTimeKind.Utc).AddTicks(7878),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 917, DateTimeKind.Utc).AddTicks(9451));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Disputes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 791, DateTimeKind.Utc).AddTicks(5433),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 917, DateTimeKind.Utc).AddTicks(2405));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 789, DateTimeKind.Utc).AddTicks(9548),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 916, DateTimeKind.Utc).AddTicks(7157));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Artisans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 789, DateTimeKind.Utc).AddTicks(357),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 915, DateTimeKind.Utc).AddTicks(8544));

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvitedPhone = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 793, DateTimeKind.Utc).AddTicks(1963)),
                    EntityStatus = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, defaultValue: "Normal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 943, DateTimeKind.Utc).AddTicks(768),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 810, DateTimeKind.Utc).AddTicks(3346));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 942, DateTimeKind.Utc).AddTicks(6242),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 810, DateTimeKind.Utc).AddTicks(179));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 942, DateTimeKind.Utc).AddTicks(1682),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 805, DateTimeKind.Utc).AddTicks(1088));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Registrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 936, DateTimeKind.Utc).AddTicks(9856),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 804, DateTimeKind.Utc).AddTicks(1200));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 935, DateTimeKind.Utc).AddTicks(8425),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 803, DateTimeKind.Utc).AddTicks(5913));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 935, DateTimeKind.Utc).AddTicks(1325),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 802, DateTimeKind.Utc).AddTicks(3523));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 919, DateTimeKind.Utc).AddTicks(1188),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 793, DateTimeKind.Utc).AddTicks(8026));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Languages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 918, DateTimeKind.Utc).AddTicks(6998),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 793, DateTimeKind.Utc).AddTicks(4748));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 917, DateTimeKind.Utc).AddTicks(9451),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 792, DateTimeKind.Utc).AddTicks(7878));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Disputes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 917, DateTimeKind.Utc).AddTicks(2405),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 791, DateTimeKind.Utc).AddTicks(5433));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 916, DateTimeKind.Utc).AddTicks(7157),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 789, DateTimeKind.Utc).AddTicks(9548));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Artisans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 915, DateTimeKind.Utc).AddTicks(8544),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 13, 15, 22, 40, 789, DateTimeKind.Utc).AddTicks(357));
        }
    }
}
