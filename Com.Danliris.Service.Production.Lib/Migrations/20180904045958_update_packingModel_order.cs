using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class update_packingModel_order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderType",
                table: "Packings",
                newName: "OrderTypeName");

            migrationBuilder.AlterColumn<string>(
                name: "Construction",
                table: "Packings",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderTypeCode",
                table: "Packings",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderTypeId",
                table: "Packings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderTypeCode",
                table: "Packings");

            migrationBuilder.DropColumn(
                name: "OrderTypeId",
                table: "Packings");

            migrationBuilder.RenameColumn(
                name: "OrderTypeName",
                table: "Packings",
                newName: "OrderType");

            migrationBuilder.AlterColumn<string>(
                name: "Construction",
                table: "Packings",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 300,
                oldNullable: true);
        }
    }
}
