using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class RemoveOldKanban : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kanbans_Kanbans_OldKanbanId",
                table: "Kanbans");

            migrationBuilder.DropIndex(
                name: "IX_Kanbans_OldKanbanId",
                table: "Kanbans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Kanbans_OldKanbanId",
                table: "Kanbans",
                column: "OldKanbanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kanbans_Kanbans_OldKanbanId",
                table: "Kanbans",
                column: "OldKanbanId",
                principalTable: "Kanbans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
