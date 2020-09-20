using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class AddKanbanInMonitoringEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KanbanCartCode",
                table: "DailyMonitoringEventProductionOrderItems",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KanbanCartNumber",
                table: "DailyMonitoringEventProductionOrderItems",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KanbanCode",
                table: "DailyMonitoringEventProductionOrderItems",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KanbanId",
                table: "DailyMonitoringEventProductionOrderItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KanbanCartCode",
                table: "DailyMonitoringEventProductionOrderItems");

            migrationBuilder.DropColumn(
                name: "KanbanCartNumber",
                table: "DailyMonitoringEventProductionOrderItems");

            migrationBuilder.DropColumn(
                name: "KanbanCode",
                table: "DailyMonitoringEventProductionOrderItems");

            migrationBuilder.DropColumn(
                name: "KanbanId",
                table: "DailyMonitoringEventProductionOrderItems");
        }
    }
}
