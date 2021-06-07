using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class AddCartNumberKanbanSnapshot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DyeingCartNumber",
                table: "KanbanSnapshots",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinishingCartNumber",
                table: "KanbanSnapshots",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreTreatmentCartNumber",
                table: "KanbanSnapshots",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrintingCartNumber",
                table: "KanbanSnapshots",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QCCartNumber",
                table: "KanbanSnapshots",
                maxLength: 1024,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DyeingCartNumber",
                table: "KanbanSnapshots");

            migrationBuilder.DropColumn(
                name: "FinishingCartNumber",
                table: "KanbanSnapshots");

            migrationBuilder.DropColumn(
                name: "PreTreatmentCartNumber",
                table: "KanbanSnapshots");

            migrationBuilder.DropColumn(
                name: "PrintingCartNumber",
                table: "KanbanSnapshots");

            migrationBuilder.DropColumn(
                name: "QCCartNumber",
                table: "KanbanSnapshots");
        }
    }
}
