using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class update_MonitoringEventModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductionOrderName",
                table: "MonitoringEventModel",
                newName: "ProductionOrderOrderNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductionOrderOrderNo",
                table: "MonitoringEventModel",
                newName: "ProductionOrderName");
        }
    }
}
