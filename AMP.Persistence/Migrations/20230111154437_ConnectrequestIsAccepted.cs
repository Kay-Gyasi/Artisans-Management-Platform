using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConnectrequestIsAccepted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 714, DateTimeKind.Utc).AddTicks(5081),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 248, DateTimeKind.Utc).AddTicks(136));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 637, DateTimeKind.Utc).AddTicks(9766),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 194, DateTimeKind.Utc).AddTicks(6384));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 630, DateTimeKind.Utc).AddTicks(5555),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 174, DateTimeKind.Utc).AddTicks(5));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Registrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 708, DateTimeKind.Utc).AddTicks(1487),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 245, DateTimeKind.Utc).AddTicks(2857));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 626, DateTimeKind.Utc).AddTicks(1966),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 172, DateTimeKind.Utc).AddTicks(4983));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 622, DateTimeKind.Utc).AddTicks(2695),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 170, DateTimeKind.Utc).AddTicks(6707));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 595, DateTimeKind.Utc).AddTicks(6784),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 159, DateTimeKind.Utc).AddTicks(5064));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 686, DateTimeKind.Utc).AddTicks(6592),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 230, DateTimeKind.Utc).AddTicks(3656));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Languages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 705, DateTimeKind.Utc).AddTicks(3071),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 243, DateTimeKind.Utc).AddTicks(3621));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Invitations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 702, DateTimeKind.Utc).AddTicks(1455),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 241, DateTimeKind.Utc).AddTicks(4369));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 699, DateTimeKind.Utc).AddTicks(5559),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 239, DateTimeKind.Utc).AddTicks(3447));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Disputes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 587, DateTimeKind.Utc).AddTicks(4903),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 157, DateTimeKind.Utc).AddTicks(7741));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 695, DateTimeKind.Utc).AddTicks(2444),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 237, DateTimeKind.Utc).AddTicks(4254));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Conversations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 672, DateTimeKind.Utc).AddTicks(3238),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 219, DateTimeKind.Utc).AddTicks(4222));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "ConnectRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 656, DateTimeKind.Utc).AddTicks(4260),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 211, DateTimeKind.Utc).AddTicks(4494));

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "ConnectRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 641, DateTimeKind.Utc).AddTicks(2379),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 198, DateTimeKind.Utc).AddTicks(8071));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Artisans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 690, DateTimeKind.Utc).AddTicks(3213),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 233, DateTimeKind.Utc).AddTicks(3896));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "ConnectRequests");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 248, DateTimeKind.Utc).AddTicks(136),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 714, DateTimeKind.Utc).AddTicks(5081));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 194, DateTimeKind.Utc).AddTicks(6384),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 637, DateTimeKind.Utc).AddTicks(9766));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 174, DateTimeKind.Utc).AddTicks(5),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 630, DateTimeKind.Utc).AddTicks(5555));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Registrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 245, DateTimeKind.Utc).AddTicks(2857),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 708, DateTimeKind.Utc).AddTicks(1487));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 172, DateTimeKind.Utc).AddTicks(4983),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 626, DateTimeKind.Utc).AddTicks(1966));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 170, DateTimeKind.Utc).AddTicks(6707),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 622, DateTimeKind.Utc).AddTicks(2695));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 159, DateTimeKind.Utc).AddTicks(5064),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 595, DateTimeKind.Utc).AddTicks(6784));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 230, DateTimeKind.Utc).AddTicks(3656),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 686, DateTimeKind.Utc).AddTicks(6592));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Languages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 243, DateTimeKind.Utc).AddTicks(3621),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 705, DateTimeKind.Utc).AddTicks(3071));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Invitations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 241, DateTimeKind.Utc).AddTicks(4369),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 702, DateTimeKind.Utc).AddTicks(1455));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 239, DateTimeKind.Utc).AddTicks(3447),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 699, DateTimeKind.Utc).AddTicks(5559));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Disputes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 157, DateTimeKind.Utc).AddTicks(7741),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 587, DateTimeKind.Utc).AddTicks(4903));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 237, DateTimeKind.Utc).AddTicks(4254),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 695, DateTimeKind.Utc).AddTicks(2444));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Conversations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 219, DateTimeKind.Utc).AddTicks(4222),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 672, DateTimeKind.Utc).AddTicks(3238));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "ConnectRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 211, DateTimeKind.Utc).AddTicks(4494),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 656, DateTimeKind.Utc).AddTicks(4260));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 198, DateTimeKind.Utc).AddTicks(8071),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 641, DateTimeKind.Utc).AddTicks(2379));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Artisans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 8, 13, 39, 27, 233, DateTimeKind.Utc).AddTicks(3896),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 690, DateTimeKind.Utc).AddTicks(3213));
        }
    }
}
