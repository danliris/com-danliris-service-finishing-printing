using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class AddColumnInExcel_AreaPretreatmentConditionCheckModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "Excel_AreaPretreatmentConditionChecks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Shift",
                table: "Excel_AreaPretreatmentConditionChecks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "Excel_AreaPretreatmentConditionChecks");

            migrationBuilder.DropColumn(
                name: "Shift",
                table: "Excel_AreaPretreatmentConditionChecks");
        }
    }
}
