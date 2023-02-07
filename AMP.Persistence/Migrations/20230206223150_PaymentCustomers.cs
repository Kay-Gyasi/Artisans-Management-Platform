using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PaymentCustomers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 869, DateTimeKind.Utc).AddTicks(5209),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 714, DateTimeKind.Utc).AddTicks(5081));

            migrationBuilder.AddColumn<DateTime>(
                name: "FundsTransferDetails_CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FundsTransferDetails_RecipientCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FundsTransferDetails_RecipientId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FundsTransferDetails_UpdatedAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 793, DateTimeKind.Utc).AddTicks(3857),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 637, DateTimeKind.Utc).AddTicks(9766));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 784, DateTimeKind.Utc).AddTicks(6107),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 630, DateTimeKind.Utc).AddTicks(5555));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Registrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 864, DateTimeKind.Utc).AddTicks(6275),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 708, DateTimeKind.Utc).AddTicks(1487));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 780, DateTimeKind.Utc).AddTicks(8613),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 626, DateTimeKind.Utc).AddTicks(1966));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 775, DateTimeKind.Utc).AddTicks(9041),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 622, DateTimeKind.Utc).AddTicks(2695));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 760, DateTimeKind.Utc).AddTicks(1045),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 595, DateTimeKind.Utc).AddTicks(6784));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 840, DateTimeKind.Utc).AddTicks(660),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 686, DateTimeKind.Utc).AddTicks(6592));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Languages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 861, DateTimeKind.Utc).AddTicks(3274),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 705, DateTimeKind.Utc).AddTicks(3071));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Invitations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 854, DateTimeKind.Utc).AddTicks(6038),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 702, DateTimeKind.Utc).AddTicks(1455));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 851, DateTimeKind.Utc).AddTicks(6974),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 699, DateTimeKind.Utc).AddTicks(5559));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Disputes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 755, DateTimeKind.Utc).AddTicks(6198),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 587, DateTimeKind.Utc).AddTicks(4903));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 847, DateTimeKind.Utc).AddTicks(4593),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 695, DateTimeKind.Utc).AddTicks(2444));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Conversations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 822, DateTimeKind.Utc).AddTicks(7266),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 672, DateTimeKind.Utc).AddTicks(3238));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "ConnectRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 808, DateTimeKind.Utc).AddTicks(6314),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 656, DateTimeKind.Utc).AddTicks(4260));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 796, DateTimeKind.Utc).AddTicks(310),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 641, DateTimeKind.Utc).AddTicks(2379));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Artisans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 843, DateTimeKind.Utc).AddTicks(3752),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 690, DateTimeKind.Utc).AddTicks(3213));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FundsTransferDetails_CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FundsTransferDetails_RecipientCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FundsTransferDetails_RecipientId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FundsTransferDetails_UpdatedAt",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 714, DateTimeKind.Utc).AddTicks(5081),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 869, DateTimeKind.Utc).AddTicks(5209));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 637, DateTimeKind.Utc).AddTicks(9766),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 793, DateTimeKind.Utc).AddTicks(3857));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 630, DateTimeKind.Utc).AddTicks(5555),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 784, DateTimeKind.Utc).AddTicks(6107));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Registrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 708, DateTimeKind.Utc).AddTicks(1487),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 864, DateTimeKind.Utc).AddTicks(6275));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 626, DateTimeKind.Utc).AddTicks(1966),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 780, DateTimeKind.Utc).AddTicks(8613));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 622, DateTimeKind.Utc).AddTicks(2695),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 775, DateTimeKind.Utc).AddTicks(9041));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 595, DateTimeKind.Utc).AddTicks(6784),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 760, DateTimeKind.Utc).AddTicks(1045));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 686, DateTimeKind.Utc).AddTicks(6592),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 840, DateTimeKind.Utc).AddTicks(660));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Languages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 705, DateTimeKind.Utc).AddTicks(3071),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 861, DateTimeKind.Utc).AddTicks(3274));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Invitations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 702, DateTimeKind.Utc).AddTicks(1455),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 854, DateTimeKind.Utc).AddTicks(6038));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 699, DateTimeKind.Utc).AddTicks(5559),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 851, DateTimeKind.Utc).AddTicks(6974));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Disputes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 587, DateTimeKind.Utc).AddTicks(4903),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 755, DateTimeKind.Utc).AddTicks(6198));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 695, DateTimeKind.Utc).AddTicks(2444),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 847, DateTimeKind.Utc).AddTicks(4593));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Conversations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 672, DateTimeKind.Utc).AddTicks(3238),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 822, DateTimeKind.Utc).AddTicks(7266));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "ConnectRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 656, DateTimeKind.Utc).AddTicks(4260),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 808, DateTimeKind.Utc).AddTicks(6314));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 641, DateTimeKind.Utc).AddTicks(2379),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 796, DateTimeKind.Utc).AddTicks(310));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Artisans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 11, 15, 44, 36, 690, DateTimeKind.Utc).AddTicks(3213),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 843, DateTimeKind.Utc).AddTicks(3752));
        }
    }
}
