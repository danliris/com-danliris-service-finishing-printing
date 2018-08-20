using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class updateModelMonitoringSpecificationMachine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "MonitoringSpecificationMachine",
                newName: "DateTimeInput");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTimeInput",
                table: "MonitoringSpecificationMachine",
                newName: "DateTime");
        }
    }
}
