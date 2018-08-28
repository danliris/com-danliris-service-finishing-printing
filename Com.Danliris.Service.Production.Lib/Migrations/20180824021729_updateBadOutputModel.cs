using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class updateBadOutputModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BadOutputMachineModel_BadOutputModel_BadOutputModelId",
                table: "BadOutputMachineModel");

            migrationBuilder.DropIndex(
                name: "IX_BadOutputMachineModel_BadOutputModelId",
                table: "BadOutputMachineModel");

            migrationBuilder.DropColumn(
                name: "BadOutputModelId",
                table: "BadOutputMachineModel");

            migrationBuilder.CreateIndex(
                name: "IX_BadOutputMachineModel_BadOutputId",
                table: "BadOutputMachineModel",
                column: "BadOutputId");

            migrationBuilder.AddForeignKey(
                name: "FK_BadOutputMachineModel_BadOutputModel_BadOutputId",
                table: "BadOutputMachineModel",
                column: "BadOutputId",
                principalTable: "BadOutputModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BadOutputMachineModel_BadOutputModel_BadOutputId",
                table: "BadOutputMachineModel");

            migrationBuilder.DropIndex(
                name: "IX_BadOutputMachineModel_BadOutputId",
                table: "BadOutputMachineModel");

            migrationBuilder.AddColumn<int>(
                name: "BadOutputModelId",
                table: "BadOutputMachineModel",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BadOutputMachineModel_BadOutputModelId",
                table: "BadOutputMachineModel",
                column: "BadOutputModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_BadOutputMachineModel_BadOutputModel_BadOutputModelId",
                table: "BadOutputMachineModel",
                column: "BadOutputModelId",
                principalTable: "BadOutputModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
