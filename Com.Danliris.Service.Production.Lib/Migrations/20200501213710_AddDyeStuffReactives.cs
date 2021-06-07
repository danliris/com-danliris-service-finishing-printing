using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class AddDyeStuffReactives : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cloth",
                table: "ColorReceipts",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "ColorReceipts",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ColorReceiptDyeStuffReactives",
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
                    Name = table.Column<string>(maxLength: 4096, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    ColorReceiptId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorReceiptDyeStuffReactives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColorReceiptDyeStuffReactives_ColorReceipts_ColorReceiptId",
                        column: x => x.ColorReceiptId,
                        principalTable: "ColorReceipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColorReceiptDyeStuffReactives_ColorReceiptId",
                table: "ColorReceiptDyeStuffReactives",
                column: "ColorReceiptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColorReceiptDyeStuffReactives");

            migrationBuilder.DropColumn(
                name: "Cloth",
                table: "ColorReceipts");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ColorReceipts");
        }
    }
}
