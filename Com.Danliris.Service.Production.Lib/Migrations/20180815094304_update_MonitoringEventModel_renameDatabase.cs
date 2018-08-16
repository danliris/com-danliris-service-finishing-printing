using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class update_MonitoringEventModel_renameDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MonitoringEventModel",
                table: "MonitoringEventModel");

            migrationBuilder.RenameTable(
                name: "MonitoringEventModel",
                newName: "MonitoringEvent");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonitoringEvent",
                table: "MonitoringEvent",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MonitoringEvent",
                table: "MonitoringEvent");

            migrationBuilder.RenameTable(
                name: "MonitoringEvent",
                newName: "MonitoringEventModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonitoringEventModel",
                table: "MonitoringEventModel",
                column: "Id");
        }
    }
}
