using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class ranameDBcontxt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BadOutputMachineModel_BadOutputModel_BadOutputId",
                table: "BadOutputMachineModel");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyOperationBadOutputReasonsModel_DailyOperationModel_DailyOperationId",
                table: "DailyOperationBadOutputReasonsModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyOperationModel",
                table: "DailyOperationModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyOperationBadOutputReasonsModel",
                table: "DailyOperationBadOutputReasonsModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BadOutputModel",
                table: "BadOutputModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BadOutputMachineModel",
                table: "BadOutputMachineModel");

            migrationBuilder.RenameTable(
                name: "DailyOperationModel",
                newName: "DailyOperation");

            migrationBuilder.RenameTable(
                name: "DailyOperationBadOutputReasonsModel",
                newName: "DailyOperationBadOutputReasons");

            migrationBuilder.RenameTable(
                name: "BadOutputModel",
                newName: "BadOutput");

            migrationBuilder.RenameTable(
                name: "BadOutputMachineModel",
                newName: "BadOutputMachine");

            migrationBuilder.RenameIndex(
                name: "IX_DailyOperationBadOutputReasonsModel_DailyOperationId",
                table: "DailyOperationBadOutputReasons",
                newName: "IX_DailyOperationBadOutputReasons_DailyOperationId");

            migrationBuilder.RenameIndex(
                name: "IX_BadOutputMachineModel_BadOutputId",
                table: "BadOutputMachine",
                newName: "IX_BadOutputMachine_BadOutputId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyOperation",
                table: "DailyOperation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyOperationBadOutputReasons",
                table: "DailyOperationBadOutputReasons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BadOutput",
                table: "BadOutput",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BadOutputMachine",
                table: "BadOutputMachine",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BadOutputMachine_BadOutput_BadOutputId",
                table: "BadOutputMachine",
                column: "BadOutputId",
                principalTable: "BadOutput",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyOperationBadOutputReasons_DailyOperation_DailyOperationId",
                table: "DailyOperationBadOutputReasons",
                column: "DailyOperationId",
                principalTable: "DailyOperation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BadOutputMachine_BadOutput_BadOutputId",
                table: "BadOutputMachine");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyOperationBadOutputReasons_DailyOperation_DailyOperationId",
                table: "DailyOperationBadOutputReasons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyOperationBadOutputReasons",
                table: "DailyOperationBadOutputReasons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyOperation",
                table: "DailyOperation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BadOutputMachine",
                table: "BadOutputMachine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BadOutput",
                table: "BadOutput");

            migrationBuilder.RenameTable(
                name: "DailyOperationBadOutputReasons",
                newName: "DailyOperationBadOutputReasonsModel");

            migrationBuilder.RenameTable(
                name: "DailyOperation",
                newName: "DailyOperationModel");

            migrationBuilder.RenameTable(
                name: "BadOutputMachine",
                newName: "BadOutputMachineModel");

            migrationBuilder.RenameTable(
                name: "BadOutput",
                newName: "BadOutputModel");

            migrationBuilder.RenameIndex(
                name: "IX_DailyOperationBadOutputReasons_DailyOperationId",
                table: "DailyOperationBadOutputReasonsModel",
                newName: "IX_DailyOperationBadOutputReasonsModel_DailyOperationId");

            migrationBuilder.RenameIndex(
                name: "IX_BadOutputMachine_BadOutputId",
                table: "BadOutputMachineModel",
                newName: "IX_BadOutputMachineModel_BadOutputId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyOperationBadOutputReasonsModel",
                table: "DailyOperationBadOutputReasonsModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyOperationModel",
                table: "DailyOperationModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BadOutputMachineModel",
                table: "BadOutputMachineModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BadOutputModel",
                table: "BadOutputModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BadOutputMachineModel_BadOutputModel_BadOutputId",
                table: "BadOutputMachineModel",
                column: "BadOutputId",
                principalTable: "BadOutputModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyOperationBadOutputReasonsModel_DailyOperationModel_DailyOperationId",
                table: "DailyOperationBadOutputReasonsModel",
                column: "DailyOperationId",
                principalTable: "DailyOperationModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
