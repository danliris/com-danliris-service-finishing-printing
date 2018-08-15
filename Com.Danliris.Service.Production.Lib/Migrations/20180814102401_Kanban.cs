using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class Kanban : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DurationEstimationAreas_DurationEstimations_DurationEstimationId",
                table: "DurationEstimationAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructionStepIndicators_InstructionSteps_StepId",
                table: "InstructionStepIndicators");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructionSteps_Instructions_InstructionId",
                table: "InstructionSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineEvents_Machine_MachineId",
                table: "MachineEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineSteps_Machine_MachineId",
                table: "MachineSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineTypeIndicators_MachineType_MachineTypeId",
                table: "MachineTypeIndicators");

            migrationBuilder.DropForeignKey(
                name: "FK_MonitoringSpecificationMachineDetails_MonitoringSpecificationMachine_MonitoringSpecificationMachineId",
                table: "MonitoringSpecificationMachineDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StepIndicators_Steps_StepId",
                table: "StepIndicators");

            migrationBuilder.CreateTable(
                name: "Kanbans",
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
                    BadOutput = table.Column<double>(nullable: false),
                    CartCartNumber = table.Column<string>(maxLength: 100, nullable: true),
                    CartCode = table.Column<string>(maxLength: 100, nullable: true),
                    CartQty = table.Column<double>(nullable: false),
                    CartPcs = table.Column<double>(nullable: false),
                    CartUomUnit = table.Column<string>(maxLength: 100, nullable: true),
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    CurrentQty = table.Column<double>(nullable: false),
                    CurrentStepIndex = table.Column<int>(nullable: false),
                    GoodOutput = table.Column<double>(nullable: false),
                    Grade = table.Column<string>(maxLength: 100, nullable: true),
                    InstructionId = table.Column<int>(nullable: false),
                    IsBadOutput = table.Column<bool>(nullable: false),
                    IsComplete = table.Column<bool>(nullable: false),
                    IsInactive = table.Column<bool>(nullable: false),
                    IsReprocess = table.Column<bool>(nullable: false),
                    OldKanbanId = table.Column<int>(nullable: false),
                    ProductionOrderId = table.Column<int>(nullable: false),
                    ProductionOrderOrderNo = table.Column<string>(maxLength: 100, nullable: true),
                    ProductionOrderOrderTypeId = table.Column<int>(nullable: false),
                    ProductionOrderOrderTypeCode = table.Column<string>(maxLength: 100, nullable: true),
                    ProductionOrderOrderTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    ProductionOrderProcessTypeId = table.Column<int>(nullable: false),
                    ProductionOrderProcessTypeCode = table.Column<string>(maxLength: 100, nullable: true),
                    ProductionOrderProcessTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    ProductionOrderMaterialId = table.Column<int>(nullable: false),
                    ProductionOrderMaterialCode = table.Column<string>(maxLength: 100, nullable: true),
                    ProductionOrderMaterialName = table.Column<string>(maxLength: 500, nullable: true),
                    ProductionOrderMaterialConstructionId = table.Column<int>(nullable: false),
                    ProductionOrderMaterialConstructionCode = table.Column<string>(maxLength: 100, nullable: true),
                    ProductionOrderMaterialConstructionName = table.Column<string>(maxLength: 500, nullable: true),
                    ProductionOrderYarnMaterialId = table.Column<int>(nullable: false),
                    ProductionOrderYarnMaterialCode = table.Column<string>(maxLength: 100, nullable: true),
                    ProductionOrderYarnMaterialName = table.Column<string>(maxLength: 250, nullable: true),
                    ProductionOrderHandlingStandard = table.Column<string>(maxLength: 100, nullable: true),
                    FinishWidth = table.Column<string>(maxLength: 100, nullable: true),
                    SelectedProductionOrderDetailId = table.Column<int>(nullable: false),
                    SelectedProductionOrderDetailColorRequest = table.Column<string>(maxLength: 250, nullable: true),
                    SelectedProductionOrderDetailColorTemplate = table.Column<string>(maxLength: 250, nullable: true),
                    SelectedProductionOrderDetailColorTypeCode = table.Column<string>(maxLength: 100, nullable: true),
                    SelectedProductionOrderDetailColorTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    SelectedProductionOrderDetailQuantity = table.Column<double>(nullable: false),
                    SelectedProductionOrderDetailUomUnit = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kanbans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kanbans_Kanbans_OldKanbanId",
                        column: x => x.OldKanbanId,
                        principalTable: "Kanbans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KanbanInstructions",
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
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    KanbanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanbanInstructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KanbanInstructions_Kanbans_KanbanId",
                        column: x => x.KanbanId,
                        principalTable: "Kanbans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KanbanSteps",
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
                    Alias = table.Column<string>(maxLength: 100, nullable: true),
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    Process = table.Column<string>(maxLength: 500, nullable: true),
                    ProcessArea = table.Column<string>(maxLength: 500, nullable: true),
                    InstructionId = table.Column<int>(nullable: false),
                    Deadline = table.Column<DateTimeOffset>(nullable: false),
                    SelectedIndex = table.Column<int>(nullable: false),
                    MachineId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanbanSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KanbanSteps_KanbanInstructions_InstructionId",
                        column: x => x.InstructionId,
                        principalTable: "KanbanInstructions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KanbanSteps_Machine_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KanbanStepIndicators",
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
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Uom = table.Column<string>(maxLength: 100, nullable: true),
                    Value = table.Column<double>(nullable: false),
                    StepId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanbanStepIndicators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KanbanStepIndicators_KanbanSteps_StepId",
                        column: x => x.StepId,
                        principalTable: "KanbanSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KanbanInstructions_KanbanId",
                table: "KanbanInstructions",
                column: "KanbanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kanbans_OldKanbanId",
                table: "Kanbans",
                column: "OldKanbanId");

            migrationBuilder.CreateIndex(
                name: "IX_KanbanStepIndicators_StepId",
                table: "KanbanStepIndicators",
                column: "StepId");

            migrationBuilder.CreateIndex(
                name: "IX_KanbanSteps_InstructionId",
                table: "KanbanSteps",
                column: "InstructionId");

            migrationBuilder.CreateIndex(
                name: "IX_KanbanSteps_MachineId",
                table: "KanbanSteps",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_DurationEstimationAreas_DurationEstimations_DurationEstimationId",
                table: "DurationEstimationAreas",
                column: "DurationEstimationId",
                principalTable: "DurationEstimations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstructionStepIndicators_InstructionSteps_StepId",
                table: "InstructionStepIndicators",
                column: "StepId",
                principalTable: "InstructionSteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstructionSteps_Instructions_InstructionId",
                table: "InstructionSteps",
                column: "InstructionId",
                principalTable: "Instructions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineEvents_Machine_MachineId",
                table: "MachineEvents",
                column: "MachineId",
                principalTable: "Machine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineSteps_Machine_MachineId",
                table: "MachineSteps",
                column: "MachineId",
                principalTable: "Machine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineTypeIndicators_MachineType_MachineTypeId",
                table: "MachineTypeIndicators",
                column: "MachineTypeId",
                principalTable: "MachineType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MonitoringSpecificationMachineDetails_MonitoringSpecificationMachine_MonitoringSpecificationMachineId",
                table: "MonitoringSpecificationMachineDetails",
                column: "MonitoringSpecificationMachineId",
                principalTable: "MonitoringSpecificationMachine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StepIndicators_Steps_StepId",
                table: "StepIndicators",
                column: "StepId",
                principalTable: "Steps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DurationEstimationAreas_DurationEstimations_DurationEstimationId",
                table: "DurationEstimationAreas");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructionStepIndicators_InstructionSteps_StepId",
                table: "InstructionStepIndicators");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructionSteps_Instructions_InstructionId",
                table: "InstructionSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineEvents_Machine_MachineId",
                table: "MachineEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineSteps_Machine_MachineId",
                table: "MachineSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineTypeIndicators_MachineType_MachineTypeId",
                table: "MachineTypeIndicators");

            migrationBuilder.DropForeignKey(
                name: "FK_MonitoringSpecificationMachineDetails_MonitoringSpecificationMachine_MonitoringSpecificationMachineId",
                table: "MonitoringSpecificationMachineDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StepIndicators_Steps_StepId",
                table: "StepIndicators");

            migrationBuilder.DropTable(
                name: "KanbanStepIndicators");

            migrationBuilder.DropTable(
                name: "KanbanSteps");

            migrationBuilder.DropTable(
                name: "KanbanInstructions");

            migrationBuilder.DropTable(
                name: "Kanbans");

            migrationBuilder.AddForeignKey(
                name: "FK_DurationEstimationAreas_DurationEstimations_DurationEstimationId",
                table: "DurationEstimationAreas",
                column: "DurationEstimationId",
                principalTable: "DurationEstimations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstructionStepIndicators_InstructionSteps_StepId",
                table: "InstructionStepIndicators",
                column: "StepId",
                principalTable: "InstructionSteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstructionSteps_Instructions_InstructionId",
                table: "InstructionSteps",
                column: "InstructionId",
                principalTable: "Instructions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineEvents_Machine_MachineId",
                table: "MachineEvents",
                column: "MachineId",
                principalTable: "Machine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineSteps_Machine_MachineId",
                table: "MachineSteps",
                column: "MachineId",
                principalTable: "Machine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineTypeIndicators_MachineType_MachineTypeId",
                table: "MachineTypeIndicators",
                column: "MachineTypeId",
                principalTable: "MachineType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MonitoringSpecificationMachineDetails_MonitoringSpecificationMachine_MonitoringSpecificationMachineId",
                table: "MonitoringSpecificationMachineDetails",
                column: "MonitoringSpecificationMachineId",
                principalTable: "MonitoringSpecificationMachine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StepIndicators_Steps_StepId",
                table: "StepIndicators",
                column: "StepId",
                principalTable: "Steps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
