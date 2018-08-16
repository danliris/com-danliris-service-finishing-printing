using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class FixForeignKey : Migration
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
                name: "FK_KanbanInstructions_Kanbans_KanbanId",
                table: "KanbanInstructions");

            migrationBuilder.DropForeignKey(
                name: "FK_KanbanStepIndicators_KanbanSteps_StepId",
                table: "KanbanStepIndicators");

            migrationBuilder.DropForeignKey(
                name: "FK_KanbanSteps_KanbanInstructions_InstructionId",
                table: "KanbanSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_KanbanSteps_Machine_MachineId",
                table: "KanbanSteps");

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
                name: "FK_KanbanInstructions_Kanbans_KanbanId",
                table: "KanbanInstructions",
                column: "KanbanId",
                principalTable: "Kanbans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanStepIndicators_KanbanSteps_StepId",
                table: "KanbanStepIndicators",
                column: "StepId",
                principalTable: "KanbanSteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanSteps_KanbanInstructions_InstructionId",
                table: "KanbanSteps",
                column: "InstructionId",
                principalTable: "KanbanInstructions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanSteps_Machine_MachineId",
                table: "KanbanSteps",
                column: "MachineId",
                principalTable: "Machine",
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
                name: "FK_KanbanInstructions_Kanbans_KanbanId",
                table: "KanbanInstructions");

            migrationBuilder.DropForeignKey(
                name: "FK_KanbanStepIndicators_KanbanSteps_StepId",
                table: "KanbanStepIndicators");

            migrationBuilder.DropForeignKey(
                name: "FK_KanbanSteps_KanbanInstructions_InstructionId",
                table: "KanbanSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_KanbanSteps_Machine_MachineId",
                table: "KanbanSteps");

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
                name: "FK_KanbanInstructions_Kanbans_KanbanId",
                table: "KanbanInstructions",
                column: "KanbanId",
                principalTable: "Kanbans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanStepIndicators_KanbanSteps_StepId",
                table: "KanbanStepIndicators",
                column: "StepId",
                principalTable: "KanbanSteps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanSteps_KanbanInstructions_InstructionId",
                table: "KanbanSteps",
                column: "InstructionId",
                principalTable: "KanbanInstructions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanSteps_Machine_MachineId",
                table: "KanbanSteps",
                column: "MachineId",
                principalTable: "Machine",
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
    }
}
