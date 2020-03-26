using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class FixModelKanbanSnapshot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KanbanCreatedUtcYear",
                table: "KanbanSnapshots",
                newName: "DOCreatedUtcYear");

            migrationBuilder.RenameColumn(
                name: "KanbanCreatedUtcMonth",
                table: "KanbanSnapshots",
                newName: "DOCreatedUtcMonth");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DOCreatedUtcYear",
                table: "KanbanSnapshots",
                newName: "KanbanCreatedUtcYear");

            migrationBuilder.RenameColumn(
                name: "DOCreatedUtcMonth",
                table: "KanbanSnapshots",
                newName: "KanbanCreatedUtcMonth");
        }
    }
}
