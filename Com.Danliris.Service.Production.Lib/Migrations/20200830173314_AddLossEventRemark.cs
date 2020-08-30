using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class AddLossEventRemark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LossEventRemarks",
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
                    Code = table.Column<string>(maxLength: 16, nullable: true),
                    LossEventProcessTypeId = table.Column<int>(nullable: false),
                    LossEventProcessTypeCode = table.Column<string>(maxLength: 512, nullable: true),
                    LossEventProcessTypeName = table.Column<string>(maxLength: 2048, nullable: true),
                    LossEventOrderTypeId = table.Column<int>(nullable: false),
                    LossEventOrderTypeCode = table.Column<string>(maxLength: 512, nullable: true),
                    LossEventOrderTypeName = table.Column<string>(maxLength: 2048, nullable: true),
                    LossEventId = table.Column<int>(nullable: false),
                    LossEventCode = table.Column<string>(maxLength: 16, nullable: true),
                    LossEventLosses = table.Column<string>(maxLength: 4096, nullable: true),
                    LossEventCategoryId = table.Column<int>(nullable: false),
                    LossEventCategoryCode = table.Column<string>(maxLength: 16, nullable: true),
                    LossEventCategoryLossesCategory = table.Column<string>(maxLength: 4096, nullable: true),
                    ProductionLossCode = table.Column<string>(maxLength: 128, nullable: true),
                    Remark = table.Column<string>(maxLength: 4096, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LossEventRemarks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LossEventRemarks");
        }
    }
}
