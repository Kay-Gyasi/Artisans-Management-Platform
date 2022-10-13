using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMP.Persistence.Migrations
{
    public partial class Userverifiedremoval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 943, DateTimeKind.Utc).AddTicks(768),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 442, DateTimeKind.Utc).AddTicks(2358));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 942, DateTimeKind.Utc).AddTicks(6242),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 441, DateTimeKind.Utc).AddTicks(8945));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 942, DateTimeKind.Utc).AddTicks(1682),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 441, DateTimeKind.Utc).AddTicks(5935));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Registrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 936, DateTimeKind.Utc).AddTicks(9856),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 437, DateTimeKind.Utc).AddTicks(7812));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 935, DateTimeKind.Utc).AddTicks(8425),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 437, DateTimeKind.Utc).AddTicks(2738));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 935, DateTimeKind.Utc).AddTicks(1325),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 436, DateTimeKind.Utc).AddTicks(5048));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 919, DateTimeKind.Utc).AddTicks(1188),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 425, DateTimeKind.Utc).AddTicks(8582));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Languages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 918, DateTimeKind.Utc).AddTicks(6998),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 417, DateTimeKind.Utc).AddTicks(3362));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 917, DateTimeKind.Utc).AddTicks(9451),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 416, DateTimeKind.Utc).AddTicks(1256));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Disputes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 917, DateTimeKind.Utc).AddTicks(2405),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 415, DateTimeKind.Utc).AddTicks(2653));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 916, DateTimeKind.Utc).AddTicks(7157),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 415, DateTimeKind.Utc).AddTicks(10));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Artisans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 915, DateTimeKind.Utc).AddTicks(8544),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 414, DateTimeKind.Utc).AddTicks(3598));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 442, DateTimeKind.Utc).AddTicks(2358),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 943, DateTimeKind.Utc).AddTicks(768));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 441, DateTimeKind.Utc).AddTicks(8945),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 942, DateTimeKind.Utc).AddTicks(6242));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 441, DateTimeKind.Utc).AddTicks(5935),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 942, DateTimeKind.Utc).AddTicks(1682));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Registrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 437, DateTimeKind.Utc).AddTicks(7812),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 936, DateTimeKind.Utc).AddTicks(9856));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 437, DateTimeKind.Utc).AddTicks(2738),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 935, DateTimeKind.Utc).AddTicks(8425));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 436, DateTimeKind.Utc).AddTicks(5048),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 935, DateTimeKind.Utc).AddTicks(1325));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 425, DateTimeKind.Utc).AddTicks(8582),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 919, DateTimeKind.Utc).AddTicks(1188));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Languages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 417, DateTimeKind.Utc).AddTicks(3362),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 918, DateTimeKind.Utc).AddTicks(6998));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 416, DateTimeKind.Utc).AddTicks(1256),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 917, DateTimeKind.Utc).AddTicks(9451));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Disputes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 415, DateTimeKind.Utc).AddTicks(2653),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 917, DateTimeKind.Utc).AddTicks(2405));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 415, DateTimeKind.Utc).AddTicks(10),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 916, DateTimeKind.Utc).AddTicks(7157));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Artisans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 414, DateTimeKind.Utc).AddTicks(3598),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 46, 23, 915, DateTimeKind.Utc).AddTicks(8544));
        }
    }
}
