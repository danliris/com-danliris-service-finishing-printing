using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class machineModel_machineEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MachineModelId",
                table: "Steps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Machine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Condition",
                table: "Machine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MachineTypeCode",
                table: "Machine",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MachineTypeId",
                table: "Machine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MachineTypeName",
                table: "Machine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manufacture",
                table: "Machine",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonthlyCapacity",
                table: "Machine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Machine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Process",
                table: "Machine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitCode",
                table: "Machine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitDivisionId",
                table: "Machine",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitDivisionName",
                table: "Machine",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Machine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "Machine",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Machine",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MachineEvents",
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
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    No = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    MachineId = table.Column<int>(nullable: false),
                    MachineModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineEvents_Machine_MachineModelId",
                        column: x => x.MachineModelId,
                        principalTable: "Machine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MonitoringEventModel",
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
                    table.PrimaryKey("PK_MonitoringEventModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Steps_MachineModelId",
                table: "Steps",
                column: "MachineModelId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineEvents_MachineModelId",
                table: "MachineEvents",
                column: "MachineModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Machine_MachineModelId",
                table: "Steps",
                column: "MachineModelId",
                principalTable: "Machine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Machine_MachineModelId",
                table: "Steps");

            migrationBuilder.DropTable(
                name: "MachineEvents");

            migrationBuilder.DropTable(
                name: "MonitoringEventModel");

            migrationBuilder.DropIndex(
                name: "IX_Steps_MachineModelId",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "MachineModelId",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "Condition",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "MachineTypeCode",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "MachineTypeId",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "MachineTypeName",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "Manufacture",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "MonthlyCapacity",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "Process",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "UnitCode",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "UnitDivisionId",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "UnitDivisionName",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Machine");
        }
    }
}
