using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class FixDataInKanbanSnapshot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KanbanCreatedUtc",
                table: "KanbanSnapshots");

            migrationBuilder.AddColumn<int>(
                name: "DyeingInputDate",
                table: "KanbanSnapshots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DyeingOutputDate",
                table: "KanbanSnapshots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FinishingInputDate",
                table: "KanbanSnapshots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FinishingOutputDate",
                table: "KanbanSnapshots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KanbanCreatedUtcMonth",
                table: "KanbanSnapshots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "KanbanCreatedUtcYear",
                table: "KanbanSnapshots",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "PreTreatmentInputDate",
                table: "KanbanSnapshots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PreTreatmentOutputDate",
                table: "KanbanSnapshots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrintingInputDate",
                table: "KanbanSnapshots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrintingOutputDate",
                table: "KanbanSnapshots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QCInputDate",
                table: "KanbanSnapshots",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QCOutputDate",
                table: "KanbanSnapshots",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DyeingInputDate",
                table: "KanbanSnapshots");

            migrationBuilder.DropColumn(
                name: "DyeingOutputDate",
                table: "KanbanSnapshots");

            migrationBuilder.DropColumn(
                name: "FinishingInputDate",
                table: "KanbanSnapshots");

            migrationBuilder.DropColumn(
                name: "FinishingOutputDate",
                table: "KanbanSnapshots");

            migrationBuilder.DropColumn(
                name: "KanbanCreatedUtcMonth",
                table: "KanbanSnapshots");

            migrationBuilder.DropColumn(
                name: "KanbanCreatedUtcYear",
                table: "KanbanSnapshots");

            migrationBuilder.DropColumn(
                name: "PreTreatmentInputDate",
                table: "KanbanSnapshots");

            migrationBuilder.DropColumn(
                name: "PreTreatmentOutputDate",
                table: "KanbanSnapshots");

            migrationBuilder.DropColumn(
                name: "PrintingInputDate",
                table: "KanbanSnapshots");

            migrationBuilder.DropColumn(
                name: "PrintingOutputDate",
                table: "KanbanSnapshots");

            migrationBuilder.DropColumn(
                name: "QCInputDate",
                table: "KanbanSnapshots");

            migrationBuilder.DropColumn(
                name: "QCOutputDate",
                table: "KanbanSnapshots");

            migrationBuilder.AddColumn<DateTime>(
                name: "KanbanCreatedUtc",
                table: "KanbanSnapshots",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
