using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class update_MonitoringSpecificationMachineModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonitoringSpecificationMachineSteps");

            migrationBuilder.DropColumn(
                name: "DivisionCode",
                table: "MonitoringSpecificationMachineModel");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "MonitoringSpecificationMachineModel");

            migrationBuilder.RenameColumn(
                name: "UnitName",
                table: "MonitoringSpecificationMachineModel",
                newName: "ProductionOrderName");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "MonitoringSpecificationMachineModel",
                newName: "ProductionOrderId");

            migrationBuilder.RenameColumn(
                name: "UnitCode",
                table: "MonitoringSpecificationMachineModel",
                newName: "MachineName");

            migrationBuilder.RenameColumn(
                name: "DivisionName",
                table: "MonitoringSpecificationMachineModel",
                newName: "MachineId");

            migrationBuilder.AddColumn<int>(
                name: "MonitoringSpecificationMachineId",
                table: "MonitoringSpecificationMachineDetailModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MonitoringSpecificationMachineDetailModel_MonitoringSpecificationMachineId",
                table: "MonitoringSpecificationMachineDetailModel",
                column: "MonitoringSpecificationMachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_MonitoringSpecificationMachineDetailModel_MonitoringSpecificationMachineModel_MonitoringSpecificationMachineId",
                table: "MonitoringSpecificationMachineDetailModel",
                column: "MonitoringSpecificationMachineId",
                principalTable: "MonitoringSpecificationMachineModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MonitoringSpecificationMachineDetailModel_MonitoringSpecificationMachineModel_MonitoringSpecificationMachineId",
                table: "MonitoringSpecificationMachineDetailModel");

            migrationBuilder.DropIndex(
                name: "IX_MonitoringSpecificationMachineDetailModel_MonitoringSpecificationMachineId",
                table: "MonitoringSpecificationMachineDetailModel");

            migrationBuilder.DropColumn(
                name: "MonitoringSpecificationMachineId",
                table: "MonitoringSpecificationMachineDetailModel");

            migrationBuilder.RenameColumn(
                name: "ProductionOrderName",
                table: "MonitoringSpecificationMachineModel",
                newName: "UnitName");

            migrationBuilder.RenameColumn(
                name: "ProductionOrderId",
                table: "MonitoringSpecificationMachineModel",
                newName: "UnitId");

            migrationBuilder.RenameColumn(
                name: "MachineName",
                table: "MonitoringSpecificationMachineModel",
                newName: "UnitCode");

            migrationBuilder.RenameColumn(
                name: "MachineId",
                table: "MonitoringSpecificationMachineModel",
                newName: "DivisionName");

            migrationBuilder.AddColumn<string>(
                name: "DivisionCode",
                table: "MonitoringSpecificationMachineModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DivisionId",
                table: "MonitoringSpecificationMachineModel",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MonitoringSpecificationMachineSteps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoringSpecificationMachineSteps", x => x.Id);
                });
        }
    }
}
