using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class AddProcessAreaLossEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProcessArea",
                table: "LossEvents",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LossEventProcessArea",
                table: "LossEventRemarks",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LossEventProcessArea",
                table: "LossEventCategories",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DailyMonitoringEvents",
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
                    Code = table.Column<string>(maxLength: 16, nullable: true),
                    MachineId = table.Column<int>(nullable: false),
                    MachineCode = table.Column<string>(maxLength: 64, nullable: true),
                    MachineName = table.Column<string>(maxLength: 4096, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Shift = table.Column<string>(maxLength: 128, nullable: true),
                    Group = table.Column<string>(maxLength: 8, nullable: true),
                    ProcessTypeId = table.Column<int>(nullable: false),
                    ProcessTypeCode = table.Column<string>(maxLength: 512, nullable: true),
                    ProcessTypeName = table.Column<string>(maxLength: 2048, nullable: true),
                    ProcessArea = table.Column<string>(maxLength: 4096, nullable: true),
                    OrderTypeId = table.Column<int>(nullable: false),
                    OrderTypeCode = table.Column<string>(maxLength: 512, nullable: true),
                    OrderTypeName = table.Column<string>(maxLength: 2048, nullable: true),
                    Kasie = table.Column<string>(maxLength: 4096, nullable: true),
                    Kasubsie = table.Column<string>(maxLength: 4096, nullable: true),
                    ElectricMechanic = table.Column<string>(maxLength: 4096, nullable: true),
                    Notes = table.Column<string>(maxLength: 4096, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyMonitoringEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyMonitoringEventLossEventItems",
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
                    LossEventRemarkId = table.Column<int>(nullable: false),
                    LossEventRemarkCode = table.Column<string>(maxLength: 16, nullable: true),
                    LossEventProductionLossCode = table.Column<string>(maxLength: 128, nullable: true),
                    LossEventLossesCategory = table.Column<string>(maxLength: 4096, nullable: true),
                    LossEventLosses = table.Column<string>(maxLength: 4096, nullable: true),
                    LossEventRemark = table.Column<string>(maxLength: 4096, nullable: true),
                    Time = table.Column<double>(nullable: false),
                    DailyMonitoringEventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyMonitoringEventLossEventItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyMonitoringEventLossEventItems_DailyMonitoringEvents_DailyMonitoringEventId",
                        column: x => x.DailyMonitoringEventId,
                        principalTable: "DailyMonitoringEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyMonitoringEventProductionOrderItems",
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
                    ProductionOrderId = table.Column<long>(nullable: false),
                    ProductionOrderCode = table.Column<string>(maxLength: 256, nullable: true),
                    ProductionOrderNo = table.Column<string>(maxLength: 256, nullable: true),
                    Speed = table.Column<double>(nullable: false),
                    Input_BQ = table.Column<double>(nullable: false),
                    Output_BS = table.Column<double>(nullable: false),
                    DailyMonitoringEventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyMonitoringEventProductionOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyMonitoringEventProductionOrderItems_DailyMonitoringEvents_DailyMonitoringEventId",
                        column: x => x.DailyMonitoringEventId,
                        principalTable: "DailyMonitoringEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyMonitoringEventLossEventItems_DailyMonitoringEventId",
                table: "DailyMonitoringEventLossEventItems",
                column: "DailyMonitoringEventId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMonitoringEventProductionOrderItems_DailyMonitoringEventId",
                table: "DailyMonitoringEventProductionOrderItems",
                column: "DailyMonitoringEventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyMonitoringEventLossEventItems");

            migrationBuilder.DropTable(
                name: "DailyMonitoringEventProductionOrderItems");

            migrationBuilder.DropTable(
                name: "DailyMonitoringEvents");

            migrationBuilder.DropColumn(
                name: "ProcessArea",
                table: "LossEvents");

            migrationBuilder.DropColumn(
                name: "LossEventProcessArea",
                table: "LossEventRemarks");

            migrationBuilder.DropColumn(
                name: "LossEventProcessArea",
                table: "LossEventCategories");
        }
    }
}
