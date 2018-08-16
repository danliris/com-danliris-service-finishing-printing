using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class addMonitoringEventModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "MonitoringEventModel",
                newName: "DateStart");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateEnd",
                table: "MonitoringEventModel",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "ProductionOrderDetailCode",
                table: "MonitoringEventModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductionOrderDetailColorRequest",
                table: "MonitoringEventModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductionOrderDetailColorTemplate",
                table: "MonitoringEventModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductionOrderDetailColorType",
                table: "MonitoringEventModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductionOrderDetailColorTypeId",
                table: "MonitoringEventModel",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ProductionOrderDetailQuantity",
                table: "MonitoringEventModel",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TimeInMilisEnd",
                table: "MonitoringEventModel",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TimeInMilisStart",
                table: "MonitoringEventModel",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "MonitoringEventModel");

            migrationBuilder.DropColumn(
                name: "ProductionOrderDetailCode",
                table: "MonitoringEventModel");

            migrationBuilder.DropColumn(
                name: "ProductionOrderDetailColorRequest",
                table: "MonitoringEventModel");

            migrationBuilder.DropColumn(
                name: "ProductionOrderDetailColorTemplate",
                table: "MonitoringEventModel");

            migrationBuilder.DropColumn(
                name: "ProductionOrderDetailColorType",
                table: "MonitoringEventModel");

            migrationBuilder.DropColumn(
                name: "ProductionOrderDetailColorTypeId",
                table: "MonitoringEventModel");

            migrationBuilder.DropColumn(
                name: "ProductionOrderDetailQuantity",
                table: "MonitoringEventModel");

            migrationBuilder.DropColumn(
                name: "TimeInMilisEnd",
                table: "MonitoringEventModel");

            migrationBuilder.DropColumn(
                name: "TimeInMilisStart",
                table: "MonitoringEventModel");

            migrationBuilder.RenameColumn(
                name: "DateStart",
                table: "MonitoringEventModel",
                newName: "DateTime");
        }
    }
}
