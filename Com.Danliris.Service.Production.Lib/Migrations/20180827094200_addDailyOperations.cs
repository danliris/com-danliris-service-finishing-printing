using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class addDailyOperations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyOperationModel",
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
                    Type = table.Column<string>(nullable: true),
                    Shift = table.Column<string>(nullable: true),
                    DateInput = table.Column<DateTimeOffset>(nullable: true),
                    TimeInput = table.Column<double>(nullable: true),
                    Input = table.Column<double>(nullable: true),
                    DateOutput = table.Column<DateTimeOffset>(nullable: true),
                    TimeOutput = table.Column<double>(nullable: true),
                    GoodOutput = table.Column<double>(nullable: true),
                    BadOutput = table.Column<double>(nullable: true),
                    StepId = table.Column<int>(nullable: false),
                    StepProcess = table.Column<int>(nullable: false),
                    KanbanId = table.Column<int>(nullable: false),
                    KanbanCode = table.Column<int>(nullable: false),
                    MachineId = table.Column<int>(nullable: false),
                    MachineCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyOperationModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DailyOperationBadOutputReasonsModel",
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
                    Length = table.Column<double>(nullable: false),
                    Action = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    BadOutputId = table.Column<string>(nullable: true),
                    BadOutputCode = table.Column<string>(nullable: true),
                    BadOutputReason = table.Column<string>(nullable: true),
                    MachineId = table.Column<string>(nullable: true),
                    MachineCode = table.Column<string>(nullable: true),
                    MachineName = table.Column<string>(nullable: true),
                    DailyOperationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyOperationBadOutputReasonsModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyOperationBadOutputReasonsModel_DailyOperationModel_DailyOperationId",
                        column: x => x.DailyOperationId,
                        principalTable: "DailyOperationModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyOperationBadOutputReasonsModel_DailyOperationId",
                table: "DailyOperationBadOutputReasonsModel",
                column: "DailyOperationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyOperationBadOutputReasonsModel");

            migrationBuilder.DropTable(
                name: "DailyOperationModel");
        }
    }
}
