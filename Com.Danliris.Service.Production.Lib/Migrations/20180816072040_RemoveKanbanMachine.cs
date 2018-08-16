using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class RemoveKanbanMachine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KanbanSteps_Machine_MachineId",
                table: "KanbanSteps");

            migrationBuilder.DropIndex(
                name: "IX_KanbanSteps_MachineId",
                table: "KanbanSteps");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_KanbanSteps_MachineId",
                table: "KanbanSteps",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanSteps_Machine_MachineId",
                table: "KanbanSteps",
                column: "MachineId",
                principalTable: "Machine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
