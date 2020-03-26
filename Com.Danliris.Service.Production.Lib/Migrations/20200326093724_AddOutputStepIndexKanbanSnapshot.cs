using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class AddOutputStepIndexKanbanSnapshot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QCStepIndex",
                table: "KanbanSnapshots",
                newName: "QCOutputStepIndex");

            migrationBuilder.RenameColumn(
                name: "PrintingStepIndex",
                table: "KanbanSnapshots",
                newName: "QCInputStepIndex");

            migrationBuilder.RenameColumn(
                name: "PreTreatmentStepIndex",
                table: "KanbanSnapshots",
                newName: "PrintingOutputStepIndex");

            migrationBuilder.RenameColumn(
                name: "FinishingStepIndex",
                table: "KanbanSnapshots",
                newName: "PrintingInputStepIndex");

            migrationBuilder.RenameColumn(
                name: "DyeingStepIndex",
                table: "KanbanSnapshots",
                newName: "PreTreatmentOutputStepIndex");

            migrationBuilder.AddColumn<int>(
                name: "DyeingInputStepIndex",
                table: "KanbanSnapshots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DyeingOutputStepIndex",
                table: "KanbanSnapshots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FinishingInputStepIndex",
                table: "KanbanSnapshots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FinishingOutputStepIndex",
                table: "KanbanSnapshots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PreTreatmentInputStepIndex",
                table: "KanbanSnapshots",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DyeingInputStepIndex",
                table: "KanbanSnapshots");

            migrationBuilder.DropColumn(
                name: "DyeingOutputStepIndex",
                table: "KanbanSnapshots");

            migrationBuilder.DropColumn(
                name: "FinishingInputStepIndex",
                table: "KanbanSnapshots");

            migrationBuilder.DropColumn(
                name: "FinishingOutputStepIndex",
                table: "KanbanSnapshots");

            migrationBuilder.DropColumn(
                name: "PreTreatmentInputStepIndex",
                table: "KanbanSnapshots");

            migrationBuilder.RenameColumn(
                name: "QCOutputStepIndex",
                table: "KanbanSnapshots",
                newName: "QCStepIndex");

            migrationBuilder.RenameColumn(
                name: "QCInputStepIndex",
                table: "KanbanSnapshots",
                newName: "PrintingStepIndex");

            migrationBuilder.RenameColumn(
                name: "PrintingOutputStepIndex",
                table: "KanbanSnapshots",
                newName: "PreTreatmentStepIndex");

            migrationBuilder.RenameColumn(
                name: "PrintingInputStepIndex",
                table: "KanbanSnapshots",
                newName: "FinishingStepIndex");

            migrationBuilder.RenameColumn(
                name: "PreTreatmentOutputStepIndex",
                table: "KanbanSnapshots",
                newName: "DyeingStepIndex");
        }
    }
}
