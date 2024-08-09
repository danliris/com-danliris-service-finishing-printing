using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class AddColumnStandardTestOnTableKanban : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StandarTestName",
                table: "Kanbans",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StandarTestName",
                table: "Kanbans");
        }
    }
}
