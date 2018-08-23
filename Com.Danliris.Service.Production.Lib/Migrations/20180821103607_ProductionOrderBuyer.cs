using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class ProductionOrderBuyer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductionOrderBuyerCode",
                table: "Kanbans",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductionOrderBuyerId",
                table: "Kanbans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductionOrderBuyerName",
                table: "Kanbans",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductionOrderBuyerCode",
                table: "Kanbans");

            migrationBuilder.DropColumn(
                name: "ProductionOrderBuyerId",
                table: "Kanbans");

            migrationBuilder.DropColumn(
                name: "ProductionOrderBuyerName",
                table: "Kanbans");
        }
    }
}
