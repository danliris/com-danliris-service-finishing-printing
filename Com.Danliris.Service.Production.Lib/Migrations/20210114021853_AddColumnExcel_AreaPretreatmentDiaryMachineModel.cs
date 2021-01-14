using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class AddColumnExcel_AreaPretreatmentDiaryMachineModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "FlowWaterPolistream",
                table: "Excel_AreaPretreatmentDiaryMachines",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FlowWaterPolistream2",
                table: "Excel_AreaPretreatmentDiaryMachines",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FlowWaterWasher",
                table: "Excel_AreaPretreatmentDiaryMachines",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlowWaterPolistream",
                table: "Excel_AreaPretreatmentDiaryMachines");

            migrationBuilder.DropColumn(
                name: "FlowWaterPolistream2",
                table: "Excel_AreaPretreatmentDiaryMachines");

            migrationBuilder.DropColumn(
                name: "FlowWaterWasher",
                table: "Excel_AreaPretreatmentDiaryMachines");
        }
    }
}
