using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class CostCalculation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CostCalculations",
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
                    ProductionOrderNo = table.Column<string>(maxLength: 64, nullable: true),
                    InstructionId = table.Column<int>(nullable: false),
                    InstructionName = table.Column<string>(maxLength: 128, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 128, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    GreigeId = table.Column<int>(nullable: false),
                    GreigeName = table.Column<string>(maxLength: 128, nullable: true),
                    PreparationValue = table.Column<double>(nullable: false),
                    CurrencyRate = table.Column<double>(nullable: false),
                    ProductionUnitValue = table.Column<double>(nullable: false),
                    TKLQuantity = table.Column<int>(nullable: false),
                    PreparationFabricWeight = table.Column<double>(nullable: false),
                    RFDFabricWeight = table.Column<double>(nullable: false),
                    ActualPrice = table.Column<double>(nullable: false),
                    CargoCost = table.Column<double>(nullable: false),
                    InsuranceCost = table.Column<double>(nullable: false),
                    Remark = table.Column<double>(maxLength: 2048, nullable: false),
                    ProductionOrderId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCalculations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CostCalculationMachines",
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
                    CostCalculationId = table.Column<int>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    MachineId = table.Column<int>(nullable: false),
                    StepProcessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCalculationMachines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCalculationMachines_CostCalculations_CostCalculationId",
                        column: x => x.CostCalculationId,
                        principalTable: "CostCalculations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CostCalculationChemicals",
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
                    CostCalculationId = table.Column<int>(nullable: false),
                    CostCalculationMachineId = table.Column<int>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    ChemicalId = table.Column<int>(nullable: false),
                    ChemicalQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCalculationChemicals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCalculationChemicals_CostCalculationMachines_CostCalculationMachineId",
                        column: x => x.CostCalculationMachineId,
                        principalTable: "CostCalculationMachines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CostCalculationChemicals_CostCalculationMachineId",
                table: "CostCalculationChemicals",
                column: "CostCalculationMachineId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCalculationMachines_CostCalculationId",
                table: "CostCalculationMachines",
                column: "CostCalculationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostCalculationChemicals");

            migrationBuilder.DropTable(
                name: "CostCalculationMachines");

            migrationBuilder.DropTable(
                name: "CostCalculations");
        }
    }
}
