using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class FabricQC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FabricQualityControlModel",
                table: "FabricQualityControlModel");

            migrationBuilder.RenameTable(
                name: "FabricQualityControlModel",
                newName: "FabricQualityControls");

            migrationBuilder.AddColumn<string>(
                name: "Buyer",
                table: "FabricQualityControls",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CartNo",
                table: "FabricQualityControls",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "FabricQualityControls",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "FabricQualityControls",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Construction",
                table: "FabricQualityControls",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateIm",
                table: "FabricQualityControls",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "FabricQualityControls",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "FabricQualityControls",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "KanbanCode",
                table: "FabricQualityControls",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KanbanId",
                table: "FabricQualityControls",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MachineNoIm",
                table: "FabricQualityControls",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OperatorIm",
                table: "FabricQualityControls",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "OrderQuantity",
                table: "FabricQualityControls",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "PackingInstruction",
                table: "FabricQualityControls",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PointLimit",
                table: "FabricQualityControls",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PointSystem",
                table: "FabricQualityControls",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ProductionOrderNo",
                table: "FabricQualityControls",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductionOrderType",
                table: "FabricQualityControls",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShiftIm",
                table: "FabricQualityControls",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Uom",
                table: "FabricQualityControls",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FabricQualityControls",
                table: "FabricQualityControls",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FabricGradeTests",
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
                    AvalLength = table.Column<double>(nullable: false),
                    FabricGradeTest = table.Column<double>(nullable: false),
                    FinalArea = table.Column<double>(nullable: false),
                    FinalGradeTest = table.Column<double>(nullable: false),
                    FinalLength = table.Column<double>(nullable: false),
                    FinalScore = table.Column<double>(nullable: false),
                    Grade = table.Column<string>(maxLength: 100, nullable: true),
                    InitLength = table.Column<double>(nullable: false),
                    PcsNo = table.Column<string>(maxLength: 100, nullable: true),
                    PointLimit = table.Column<double>(nullable: false),
                    PointSystem = table.Column<double>(nullable: false),
                    SampleLength = table.Column<double>(nullable: false),
                    Score = table.Column<double>(nullable: false),
                    Type = table.Column<string>(maxLength: 100, nullable: true),
                    Width = table.Column<double>(nullable: false),
                    FabricQualityControlId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FabricGradeTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FabricGradeTests_FabricQualityControls_FabricQualityControlId",
                        column: x => x.FabricQualityControlId,
                        principalTable: "FabricQualityControls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Criterion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<int>(nullable: false),
                    Group = table.Column<int>(nullable: false),
                    Name = table.Column<int>(nullable: false),
                    ScoreA = table.Column<double>(nullable: false),
                    ScoreB = table.Column<double>(nullable: false),
                    ScoreC = table.Column<double>(nullable: false),
                    ScoreD = table.Column<double>(nullable: false),
                    FabricGradeTestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criterion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Criterion_FabricGradeTests_FabricGradeTestId",
                        column: x => x.FabricGradeTestId,
                        principalTable: "FabricGradeTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Criterion_FabricGradeTestId",
                table: "Criterion",
                column: "FabricGradeTestId");

            migrationBuilder.CreateIndex(
                name: "IX_FabricGradeTests_FabricQualityControlId",
                table: "FabricGradeTests",
                column: "FabricQualityControlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Criterion");

            migrationBuilder.DropTable(
                name: "FabricGradeTests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FabricQualityControls",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "Buyer",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "CartNo",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "Construction",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "DateIm",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "Group",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "KanbanCode",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "KanbanId",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "MachineNoIm",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "OperatorIm",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "OrderQuantity",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "PackingInstruction",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "PointLimit",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "PointSystem",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "ProductionOrderNo",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "ProductionOrderType",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "ShiftIm",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "Uom",
                table: "FabricQualityControls");

            migrationBuilder.RenameTable(
                name: "FabricQualityControls",
                newName: "FabricQualityControlModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FabricQualityControlModel",
                table: "FabricQualityControlModel",
                column: "Id");
        }
    }
}
