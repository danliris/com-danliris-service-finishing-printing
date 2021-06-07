using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class AddNewColumnInMachine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Electric",
                table: "Machine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LPG",
                table: "Machine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Solar",
                table: "Machine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Steam",
                table: "Machine",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Water",
                table: "Machine",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Electric",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "LPG",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "Solar",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "Steam",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "Water",
                table: "Machine");
        }
    }
}
