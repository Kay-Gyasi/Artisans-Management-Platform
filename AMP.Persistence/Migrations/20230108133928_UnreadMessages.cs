using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UnreadMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 248, DateTimeKind.Utc).AddTicks(136),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 772, DateTimeKind.Utc).AddTicks(441));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 194, DateTimeKind.Utc).AddTicks(6384),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 713, DateTimeKind.Utc).AddTicks(4811));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 174, DateTimeKind.Utc).AddTicks(5),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 705, DateTimeKind.Utc).AddTicks(7819));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Registrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 245, DateTimeKind.Utc).AddTicks(2857),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 768, DateTimeKind.Utc).AddTicks(7383));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 172, DateTimeKind.Utc).AddTicks(4983),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 698, DateTimeKind.Utc).AddTicks(9080));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 170, DateTimeKind.Utc).AddTicks(6707),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 676, DateTimeKind.Utc).AddTicks(461));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 159, DateTimeKind.Utc).AddTicks(5064),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 651, DateTimeKind.Utc).AddTicks(2932));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 230, DateTimeKind.Utc).AddTicks(3656),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 753, DateTimeKind.Utc).AddTicks(4077));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Languages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 243, DateTimeKind.Utc).AddTicks(3621),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 766, DateTimeKind.Utc).AddTicks(7667));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Invitations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 241, DateTimeKind.Utc).AddTicks(4369),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 764, DateTimeKind.Utc).AddTicks(8503));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 239, DateTimeKind.Utc).AddTicks(3447),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 762, DateTimeKind.Utc).AddTicks(6957));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Disputes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 157, DateTimeKind.Utc).AddTicks(7741),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 648, DateTimeKind.Utc).AddTicks(2293));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 237, DateTimeKind.Utc).AddTicks(4254),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 760, DateTimeKind.Utc).AddTicks(2726));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Conversations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 219, DateTimeKind.Utc).AddTicks(4222),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 742, DateTimeKind.Utc).AddTicks(5194));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "ConnectRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 211, DateTimeKind.Utc).AddTicks(4494),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 730, DateTimeKind.Utc).AddTicks(2585));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 198, DateTimeKind.Utc).AddTicks(8071),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 717, DateTimeKind.Utc).AddTicks(2394));

            migrationBuilder.AddColumn<bool>(
                name: "IsSeen",
                table: "ChatMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Artisans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 233, DateTimeKind.Utc).AddTicks(3896),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 756, DateTimeKind.Utc).AddTicks(7770));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSeen",
                table: "ChatMessages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 772, DateTimeKind.Utc).AddTicks(441),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 248, DateTimeKind.Utc).AddTicks(136));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 713, DateTimeKind.Utc).AddTicks(4811),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 194, DateTimeKind.Utc).AddTicks(6384));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 705, DateTimeKind.Utc).AddTicks(7819),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 174, DateTimeKind.Utc).AddTicks(5));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Registrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 768, DateTimeKind.Utc).AddTicks(7383),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 245, DateTimeKind.Utc).AddTicks(2857));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 698, DateTimeKind.Utc).AddTicks(9080),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 172, DateTimeKind.Utc).AddTicks(4983));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 676, DateTimeKind.Utc).AddTicks(461),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 170, DateTimeKind.Utc).AddTicks(6707));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 651, DateTimeKind.Utc).AddTicks(2932),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 159, DateTimeKind.Utc).AddTicks(5064));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 753, DateTimeKind.Utc).AddTicks(4077),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 230, DateTimeKind.Utc).AddTicks(3656));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Languages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 766, DateTimeKind.Utc).AddTicks(7667),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 243, DateTimeKind.Utc).AddTicks(3621));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Invitations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 764, DateTimeKind.Utc).AddTicks(8503),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 241, DateTimeKind.Utc).AddTicks(4369));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 762, DateTimeKind.Utc).AddTicks(6957),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 239, DateTimeKind.Utc).AddTicks(3447));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Disputes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 648, DateTimeKind.Utc).AddTicks(2293),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 157, DateTimeKind.Utc).AddTicks(7741));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 760, DateTimeKind.Utc).AddTicks(2726),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 237, DateTimeKind.Utc).AddTicks(4254));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Conversations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 742, DateTimeKind.Utc).AddTicks(5194),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 219, DateTimeKind.Utc).AddTicks(4222));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "ConnectRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 730, DateTimeKind.Utc).AddTicks(2585),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 211, DateTimeKind.Utc).AddTicks(4494));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 717, DateTimeKind.Utc).AddTicks(2394),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 198, DateTimeKind.Utc).AddTicks(8071));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Artisans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 7, 13, 7, 39, 756, DateTimeKind.Utc).AddTicks(7770),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 233, DateTimeKind.Utc).AddTicks(3896));
        }
    }
}
