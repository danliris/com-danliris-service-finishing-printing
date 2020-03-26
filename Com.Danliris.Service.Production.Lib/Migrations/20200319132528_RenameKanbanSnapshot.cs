using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class RenameKanbanSnapshot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_KanbanSnapshotModels",
                table: "KanbanSnapshotModels");

            migrationBuilder.RenameTable(
                name: "KanbanSnapshotModels",
                newName: "KanbanSnapshots");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KanbanSnapshots",
                table: "KanbanSnapshots",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_KanbanSnapshots",
                table: "KanbanSnapshots");

            migrationBuilder.RenameTable(
                name: "KanbanSnapshots",
                newName: "KanbanSnapshotModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KanbanSnapshotModels",
                table: "KanbanSnapshotModels",
                column: "Id");
        }
    }
}
