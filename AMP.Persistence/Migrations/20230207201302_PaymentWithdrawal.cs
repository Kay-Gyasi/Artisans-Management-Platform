using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PaymentWithdrawal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 366, DateTimeKind.Utc).AddTicks(953),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 869, DateTimeKind.Utc).AddTicks(5209));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 314, DateTimeKind.Utc).AddTicks(3212),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 793, DateTimeKind.Utc).AddTicks(3857));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 307, DateTimeKind.Utc).AddTicks(9658),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 784, DateTimeKind.Utc).AddTicks(6107));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Registrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 363, DateTimeKind.Utc).AddTicks(1682),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 864, DateTimeKind.Utc).AddTicks(6275));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 304, DateTimeKind.Utc).AddTicks(7648),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 780, DateTimeKind.Utc).AddTicks(8613));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 294, DateTimeKind.Utc).AddTicks(5502),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 775, DateTimeKind.Utc).AddTicks(9041));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 279, DateTimeKind.Utc).AddTicks(6297),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 760, DateTimeKind.Utc).AddTicks(1045));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 348, DateTimeKind.Utc).AddTicks(6247),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 840, DateTimeKind.Utc).AddTicks(660));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Languages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 361, DateTimeKind.Utc).AddTicks(3518),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 861, DateTimeKind.Utc).AddTicks(3274));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Invitations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 359, DateTimeKind.Utc).AddTicks(2818),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 854, DateTimeKind.Utc).AddTicks(6038));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 357, DateTimeKind.Utc).AddTicks(4050),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 851, DateTimeKind.Utc).AddTicks(6974));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Disputes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 276, DateTimeKind.Utc).AddTicks(4518),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 755, DateTimeKind.Utc).AddTicks(6198));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 355, DateTimeKind.Utc).AddTicks(2994),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 847, DateTimeKind.Utc).AddTicks(4593));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Conversations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 337, DateTimeKind.Utc).AddTicks(6242),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 822, DateTimeKind.Utc).AddTicks(7266));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "ConnectRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 327, DateTimeKind.Utc).AddTicks(9078),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 808, DateTimeKind.Utc).AddTicks(6314));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 316, DateTimeKind.Utc).AddTicks(6345),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 796, DateTimeKind.Utc).AddTicks(310));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Artisans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 351, DateTimeKind.Utc).AddTicks(7648),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 843, DateTimeKind.Utc).AddTicks(3752));

            migrationBuilder.CreateTable(
                name: "PaymentWithdrawal",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    UserId = table.Column<string>(type: "varchar(36)", nullable: false),
                    MomoTransferNumber = table.Column<string>(name: "MomoTransfer_Number", type: "nvarchar(max)", nullable: true),
                    MomoTransferNetworkProvider = table.Column<string>(name: "MomoTransfer_NetworkProvider", type: "nvarchar(max)", nullable: true),
                    RowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 297, DateTimeKind.Utc).AddTicks(4247)),
                    EntityStatus = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false, defaultValue: "Normal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentWithdrawal", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_PaymentWithdrawal_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentWithdrawal_RowId",
                table: "PaymentWithdrawal",
                column: "RowId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentWithdrawal_UserId",
                table: "PaymentWithdrawal",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentWithdrawal");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 869, DateTimeKind.Utc).AddTicks(5209),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 366, DateTimeKind.Utc).AddTicks(953));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Services",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 793, DateTimeKind.Utc).AddTicks(3857),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 314, DateTimeKind.Utc).AddTicks(3212));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 784, DateTimeKind.Utc).AddTicks(6107),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 307, DateTimeKind.Utc).AddTicks(9658));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Registrations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 864, DateTimeKind.Utc).AddTicks(6275),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 363, DateTimeKind.Utc).AddTicks(1682));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Ratings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 780, DateTimeKind.Utc).AddTicks(8613),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 304, DateTimeKind.Utc).AddTicks(7648));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 775, DateTimeKind.Utc).AddTicks(9041),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 294, DateTimeKind.Utc).AddTicks(5502));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 760, DateTimeKind.Utc).AddTicks(1045),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 279, DateTimeKind.Utc).AddTicks(6297));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 840, DateTimeKind.Utc).AddTicks(660),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 348, DateTimeKind.Utc).AddTicks(6247));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Languages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 861, DateTimeKind.Utc).AddTicks(3274),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 361, DateTimeKind.Utc).AddTicks(3518));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Invitations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 854, DateTimeKind.Utc).AddTicks(6038),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 359, DateTimeKind.Utc).AddTicks(2818));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Images",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 851, DateTimeKind.Utc).AddTicks(6974),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 357, DateTimeKind.Utc).AddTicks(4050));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Disputes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 755, DateTimeKind.Utc).AddTicks(6198),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 276, DateTimeKind.Utc).AddTicks(4518));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 847, DateTimeKind.Utc).AddTicks(4593),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 355, DateTimeKind.Utc).AddTicks(2994));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Conversations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 822, DateTimeKind.Utc).AddTicks(7266),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 337, DateTimeKind.Utc).AddTicks(6242));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "ConnectRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 808, DateTimeKind.Utc).AddTicks(6314),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 327, DateTimeKind.Utc).AddTicks(9078));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 796, DateTimeKind.Utc).AddTicks(310),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 316, DateTimeKind.Utc).AddTicks(6345));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateModified",
                table: "Artisans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 6, 22, 31, 48, 843, DateTimeKind.Utc).AddTicks(3752),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 7, 20, 13, 1, 351, DateTimeKind.Utc).AddTicks(7648));
        }
    }
}
