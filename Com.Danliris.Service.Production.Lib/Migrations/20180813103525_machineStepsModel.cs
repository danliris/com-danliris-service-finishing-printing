using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class machineStepsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineEvents_Machine_MachineModelId",
                table: "MachineEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Machine_MachineModelId",
                table: "Steps");

            migrationBuilder.DropIndex(
                name: "IX_Steps_MachineModelId",
                table: "Steps");

            migrationBuilder.DropIndex(
                name: "IX_MachineEvents_MachineModelId",
                table: "MachineEvents");

            migrationBuilder.DropColumn(
                name: "MachineModelId",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "MachineModelId",
                table: "MachineEvents");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "MonitoringSpecificationMachineModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DivisionCode",
                table: "MonitoringSpecificationMachineModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DivisionId",
                table: "MonitoringSpecificationMachineModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DivisionName",
                table: "MonitoringSpecificationMachineModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitCode",
                table: "MonitoringSpecificationMachineModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitId",
                table: "MonitoringSpecificationMachineModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "MonitoringSpecificationMachineModel",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MachineSteps",
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
                    Alias = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Process = table.Column<string>(nullable: true),
                    ProcessArea = table.Column<string>(nullable: true),
                    MachineId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineSteps_Machine_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonitoringSpecificationMachineSteps",
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
                    DeletedAgent = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoringSpecificationMachineSteps", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MachineEvents_MachineId",
                table: "MachineEvents",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSteps_MachineId",
                table: "MachineSteps",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineEvents_Machine_MachineId",
                table: "MachineEvents",
                column: "MachineId",
                principalTable: "Machine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineEvents_Machine_MachineId",
                table: "MachineEvents");

            migrationBuilder.DropTable(
                name: "MachineSteps");

            migrationBuilder.DropTable(
                name: "MonitoringSpecificationMachineSteps");

            migrationBuilder.DropIndex(
                name: "IX_MachineEvents_MachineId",
                table: "MachineEvents");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "MonitoringSpecificationMachineModel");

            migrationBuilder.DropColumn(
                name: "DivisionCode",
                table: "MonitoringSpecificationMachineModel");

            migrationBuilder.DropColumn(
                name: "DivisionId",
                table: "MonitoringSpecificationMachineModel");

            migrationBuilder.DropColumn(
                name: "DivisionName",
                table: "MonitoringSpecificationMachineModel");

            migrationBuilder.DropColumn(
                name: "UnitCode",
                table: "MonitoringSpecificationMachineModel");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "MonitoringSpecificationMachineModel");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "MonitoringSpecificationMachineModel");

            migrationBuilder.AddColumn<int>(
                name: "MachineModelId",
                table: "Steps",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MachineModelId",
                table: "MachineEvents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Steps_MachineModelId",
                table: "Steps",
                column: "MachineModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineEvents_MachineModelId",
                table: "MachineEvents",
                column: "MachineModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineEvents_Machine_MachineModelId",
                table: "MachineEvents",
                column: "MachineModelId",
                principalTable: "Machine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Machine_MachineModelId",
                table: "Steps",
                column: "MachineModelId",
                principalTable: "Machine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
