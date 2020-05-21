using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class EnhanceStrikeOff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorReceiptId",
                table: "StrikeOffItems");

            migrationBuilder.RenameColumn(
                name: "ColorReceiptColorCode",
                table: "StrikeOffItems",
                newName: "ColorCode");

            migrationBuilder.AddColumn<string>(
                name: "Cloth",
                table: "StrikeOffs",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "StrikeOffs",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StrikeOffItemChemicalItems",
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
                    Name = table.Column<string>(maxLength: 2048, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    StrikeOffItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrikeOffItemChemicalItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StrikeOffItemChemicalItems_StrikeOffItems_StrikeOffItemId",
                        column: x => x.StrikeOffItemId,
                        principalTable: "StrikeOffItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StrikeOffItemDyeStuffItems",
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
                    ProductId = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(maxLength: 256, nullable: true),
                    ProductCode = table.Column<string>(maxLength: 128, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    StrikeOffItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrikeOffItemDyeStuffItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StrikeOffItemDyeStuffItems_StrikeOffItems_StrikeOffItemId",
                        column: x => x.StrikeOffItemId,
                        principalTable: "StrikeOffItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StrikeOffItemChemicalItems_StrikeOffItemId",
                table: "StrikeOffItemChemicalItems",
                column: "StrikeOffItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StrikeOffItemDyeStuffItems_StrikeOffItemId",
                table: "StrikeOffItemDyeStuffItems",
                column: "StrikeOffItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StrikeOffItemChemicalItems");

            migrationBuilder.DropTable(
                name: "StrikeOffItemDyeStuffItems");

            migrationBuilder.DropColumn(
                name: "Cloth",
                table: "StrikeOffs");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "StrikeOffs");

            migrationBuilder.RenameColumn(
                name: "ColorCode",
                table: "StrikeOffItems",
                newName: "ColorReceiptColorCode");

            migrationBuilder.AddColumn<int>(
                name: "ColorReceiptId",
                table: "StrikeOffItems",
                nullable: false,
                defaultValue: 0);
        }
    }
}
