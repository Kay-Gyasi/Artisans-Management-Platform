using Microsoft.EntityFrameworkCore.Migrations;

namespace AMP.Persistence.Migrations
{
    public partial class DisputesOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disputes_Artisans_ArtisanId",
                table: "Disputes");

            migrationBuilder.RenameColumn(
                name: "ArtisanId",
                table: "Disputes",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Disputes_ArtisanId",
                table: "Disputes",
                newName: "IX_Disputes_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Disputes_Orders_OrderId",
                table: "Disputes",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disputes_Orders_OrderId",
                table: "Disputes");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Disputes",
                newName: "ArtisanId");

            migrationBuilder.RenameIndex(
                name: "IX_Disputes_OrderId",
                table: "Disputes",
                newName: "IX_Disputes_ArtisanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Disputes_Artisans_ArtisanId",
                table: "Disputes",
                column: "ArtisanId",
                principalTable: "Artisans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
