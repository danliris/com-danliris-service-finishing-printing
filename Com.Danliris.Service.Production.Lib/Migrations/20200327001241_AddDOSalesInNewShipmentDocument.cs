using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class AddDOSalesInNewShipmentDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryCode",
                table: "NewShipmentDocumentModel");

            migrationBuilder.AddColumn<int>(
                name: "DOSalesId",
                table: "NewShipmentDocumentModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DOSalesNo",
                table: "NewShipmentDocumentModel",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOSalesId",
                table: "NewShipmentDocumentModel");

            migrationBuilder.DropColumn(
                name: "DOSalesNo",
                table: "NewShipmentDocumentModel");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryCode",
                table: "NewShipmentDocumentModel",
                maxLength: 250,
                nullable: true);
        }
    }
}
