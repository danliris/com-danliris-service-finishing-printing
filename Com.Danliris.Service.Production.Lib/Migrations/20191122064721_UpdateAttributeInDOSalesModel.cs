using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class UpdateAttributeInDOSalesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryType",
                table: "DOSalesItems");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DOSalesItemDetails");

            migrationBuilder.RenameColumn(
                name: "FinishedProductType",
                table: "DOSalesItems",
                newName: "StorageCode");

            migrationBuilder.AddColumn<int>(
                name: "StorageId",
                table: "DOSalesItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StorageName",
                table: "DOSalesItems",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UnitCode",
                table: "DOSalesItemDetails",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "DOSalesItemDetails",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "DOSalesItems");

            migrationBuilder.DropColumn(
                name: "StorageName",
                table: "DOSalesItems");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "DOSalesItemDetails");

            migrationBuilder.RenameColumn(
                name: "StorageCode",
                table: "DOSalesItems",
                newName: "FinishedProductType");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryType",
                table: "DOSalesItems",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UnitCode",
                table: "DOSalesItemDetails",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "DOSalesItemDetails",
                nullable: true);
        }
    }
}
