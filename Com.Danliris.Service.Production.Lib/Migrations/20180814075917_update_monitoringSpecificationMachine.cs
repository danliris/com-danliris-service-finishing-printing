using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class update_monitoringSpecificationMachine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductionOrderName",
                table: "MonitoringSpecificationMachine",
                newName: "ProductionOrderNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductionOrderNo",
                table: "MonitoringSpecificationMachine",
                newName: "ProductionOrderName");
        }
    }
}
