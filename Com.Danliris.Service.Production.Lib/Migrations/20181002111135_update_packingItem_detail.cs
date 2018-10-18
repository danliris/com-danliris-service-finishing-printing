using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class update_packingItem_detail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "PackingReceiptItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Uom",
                table: "PackingReceiptItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "PackingReceiptItem");

            migrationBuilder.DropColumn(
                name: "Uom",
                table: "PackingReceiptItem");
        }
    }
}
