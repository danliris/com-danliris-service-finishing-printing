using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class FixProductAndStorageInReturToQC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "ReturQuantity",
                table: "ReturToQCItemDetails",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "QuantityBefore",
                table: "ReturToQCItemDetails",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "ReturToQCItemDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StorageCode",
                table: "ReturToQCItemDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StorageName",
                table: "ReturToQCItemDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "ReturToQCItemDetails");

            migrationBuilder.DropColumn(
                name: "StorageCode",
                table: "ReturToQCItemDetails");

            migrationBuilder.DropColumn(
                name: "StorageName",
                table: "ReturToQCItemDetails");

            migrationBuilder.AlterColumn<int>(
                name: "ReturQuantity",
                table: "ReturToQCItemDetails",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "QuantityBefore",
                table: "ReturToQCItemDetails",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
