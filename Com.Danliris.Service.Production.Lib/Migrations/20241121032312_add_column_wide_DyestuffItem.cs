using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class add_column_wide_DyestuffItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Wide",
                table: "DyestuffChemicalUsageReceiptItems",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Wide",
                table: "DyestuffChemicalUsageReceiptItems");
        }
    }
}
