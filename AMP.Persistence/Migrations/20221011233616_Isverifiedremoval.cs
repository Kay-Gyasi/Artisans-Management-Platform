using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMP.Persistence.Migrations
{
    public partial class Isverifiedremoval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "Customers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 442, DateTimeKind.Utc).AddTicks(2358),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 712, DateTimeKind.Utc).AddTicks(2197));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 441, DateTimeKind.Utc).AddTicks(8945),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 711, DateTimeKind.Utc).AddTicks(9585));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 441, DateTimeKind.Utc).AddTicks(5935),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 711, DateTimeKind.Utc).AddTicks(5838));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Registrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 437, DateTimeKind.Utc).AddTicks(7812),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 708, DateTimeKind.Utc).AddTicks(2156));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 437, DateTimeKind.Utc).AddTicks(2738),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 707, DateTimeKind.Utc).AddTicks(3067));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 436, DateTimeKind.Utc).AddTicks(5048),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 706, DateTimeKind.Utc).AddTicks(6641));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 425, DateTimeKind.Utc).AddTicks(8582),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 696, DateTimeKind.Utc).AddTicks(9413));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Languages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 417, DateTimeKind.Utc).AddTicks(3362),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 696, DateTimeKind.Utc).AddTicks(8229));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 416, DateTimeKind.Utc).AddTicks(1256),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 696, DateTimeKind.Utc).AddTicks(6563));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Disputes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 415, DateTimeKind.Utc).AddTicks(2653),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 696, DateTimeKind.Utc).AddTicks(3245));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 415, DateTimeKind.Utc).AddTicks(10),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 696, DateTimeKind.Utc).AddTicks(637));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Artisans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 414, DateTimeKind.Utc).AddTicks(3598),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 695, DateTimeKind.Utc).AddTicks(6353));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 712, DateTimeKind.Utc).AddTicks(2197),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 442, DateTimeKind.Utc).AddTicks(2358));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 711, DateTimeKind.Utc).AddTicks(9585),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 441, DateTimeKind.Utc).AddTicks(8945));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 711, DateTimeKind.Utc).AddTicks(5838),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 441, DateTimeKind.Utc).AddTicks(5935));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Registrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 708, DateTimeKind.Utc).AddTicks(2156),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 437, DateTimeKind.Utc).AddTicks(7812));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 707, DateTimeKind.Utc).AddTicks(3067),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 437, DateTimeKind.Utc).AddTicks(2738));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 706, DateTimeKind.Utc).AddTicks(6641),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 436, DateTimeKind.Utc).AddTicks(5048));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 696, DateTimeKind.Utc).AddTicks(9413),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 425, DateTimeKind.Utc).AddTicks(8582));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Languages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 696, DateTimeKind.Utc).AddTicks(8229),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 417, DateTimeKind.Utc).AddTicks(3362));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 696, DateTimeKind.Utc).AddTicks(6563),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 416, DateTimeKind.Utc).AddTicks(1256));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Disputes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 696, DateTimeKind.Utc).AddTicks(3245),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 415, DateTimeKind.Utc).AddTicks(2653));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 696, DateTimeKind.Utc).AddTicks(637),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 415, DateTimeKind.Utc).AddTicks(10));

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Artisans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 10, 11, 21, 54, 12, 695, DateTimeKind.Utc).AddTicks(6353),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 10, 11, 23, 36, 16, 414, DateTimeKind.Utc).AddTicks(3598));
        }
    }
}
