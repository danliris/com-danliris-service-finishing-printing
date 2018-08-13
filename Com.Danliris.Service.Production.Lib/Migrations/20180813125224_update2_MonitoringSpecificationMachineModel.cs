using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class update2_MonitoringSpecificationMachineModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonitoringSpecificationMachineDetailModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MonitoringSpecificationMachineModel",
                table: "MonitoringSpecificationMachineModel");

            migrationBuilder.RenameTable(
                name: "MonitoringSpecificationMachineModel",
                newName: "MonitoringSpecificationMachine");

            migrationBuilder.AddColumn<string>(
                name: "CartNumber",
                table: "MonitoringEventModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "MonitoringEventModel",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateTime",
                table: "MonitoringEventModel",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "MachineEventCategory",
                table: "MonitoringEventModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MachineEventCode",
                table: "MonitoringEventModel",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MachineEventId",
                table: "MonitoringEventModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MachineEventName",
                table: "MonitoringEventModel",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MachineId",
                table: "MonitoringEventModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MachineName",
                table: "MonitoringEventModel",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductionOrderId",
                table: "MonitoringEventModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductionOrderName",
                table: "MonitoringEventModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "MonitoringEventModel",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductionOrderId",
                table: "MonitoringSpecificationMachine",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MachineId",
                table: "MonitoringSpecificationMachine",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonitoringSpecificationMachine",
                table: "MonitoringSpecificationMachine",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MonitoringSpecificationMachineDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    Indicator = table.Column<string>(nullable: true),
                    DataType = table.Column<string>(nullable: true),
                    DefaultValue = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: false),
                    Uom = table.Column<string>(nullable: true),
                    MonitoringSpecificationMachineId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoringSpecificationMachineDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonitoringSpecificationMachineDetails_MonitoringSpecificationMachine_MonitoringSpecificationMachineId",
                        column: x => x.MonitoringSpecificationMachineId,
                        principalTable: "MonitoringSpecificationMachine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonitoringSpecificationMachineDetails_MonitoringSpecificationMachineId",
                table: "MonitoringSpecificationMachineDetails",
                column: "MonitoringSpecificationMachineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonitoringSpecificationMachineDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MonitoringSpecificationMachine",
                table: "MonitoringSpecificationMachine");

            migrationBuilder.DropColumn(
                name: "CartNumber",
                table: "MonitoringEventModel");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "MonitoringEventModel");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "MonitoringEventModel");

            migrationBuilder.DropColumn(
                name: "MachineEventCategory",
                table: "MonitoringEventModel");

            migrationBuilder.DropColumn(
                name: "MachineEventCode",
                table: "MonitoringEventModel");

            migrationBuilder.DropColumn(
                name: "MachineEventId",
                table: "MonitoringEventModel");

            migrationBuilder.DropColumn(
                name: "MachineEventName",
                table: "MonitoringEventModel");

            migrationBuilder.DropColumn(
                name: "MachineId",
                table: "MonitoringEventModel");

            migrationBuilder.DropColumn(
                name: "MachineName",
                table: "MonitoringEventModel");

            migrationBuilder.DropColumn(
                name: "ProductionOrderId",
                table: "MonitoringEventModel");

            migrationBuilder.DropColumn(
                name: "ProductionOrderName",
                table: "MonitoringEventModel");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "MonitoringEventModel");

            migrationBuilder.RenameTable(
                name: "MonitoringSpecificationMachine",
                newName: "MonitoringSpecificationMachineModel");

            migrationBuilder.AlterColumn<string>(
                name: "ProductionOrderId",
                table: "MonitoringSpecificationMachineModel",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "MachineId",
                table: "MonitoringSpecificationMachineModel",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonitoringSpecificationMachineModel",
                table: "MonitoringSpecificationMachineModel",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MonitoringSpecificationMachineDetailModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DataType = table.Column<string>(nullable: true),
                    DefaultValue = table.Column<string>(nullable: true),
                    DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    Indicator = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    MonitoringSpecificationMachineId = table.Column<int>(nullable: false),
                    Uom = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoringSpecificationMachineDetailModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonitoringSpecificationMachineDetailModel_MonitoringSpecificationMachineModel_MonitoringSpecificationMachineId",
                        column: x => x.MonitoringSpecificationMachineId,
                        principalTable: "MonitoringSpecificationMachineModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonitoringSpecificationMachineDetailModel_MonitoringSpecificationMachineId",
                table: "MonitoringSpecificationMachineDetailModel",
                column: "MonitoringSpecificationMachineId");
        }
    }
}
