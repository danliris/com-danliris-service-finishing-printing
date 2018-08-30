using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class updateModelDaily_changeDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MachineId",
                table: "DailyOperationBadOutputReasons",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BadOutputId",
                table: "DailyOperationBadOutputReasons",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyOperation_KanbanId",
                table: "DailyOperation",
                column: "KanbanId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyOperation_MachineId",
                table: "DailyOperation",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyOperation_Kanbans_KanbanId",
                table: "DailyOperation",
                column: "KanbanId",
                principalTable: "Kanbans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyOperation_Machine_MachineId",
                table: "DailyOperation",
                column: "MachineId",
                principalTable: "Machine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyOperation_Kanbans_KanbanId",
                table: "DailyOperation");

            migrationBuilder.DropForeignKey(
                name: "FK_DailyOperation_Machine_MachineId",
                table: "DailyOperation");

            migrationBuilder.DropIndex(
                name: "IX_DailyOperation_KanbanId",
                table: "DailyOperation");

            migrationBuilder.DropIndex(
                name: "IX_DailyOperation_MachineId",
                table: "DailyOperation");

            migrationBuilder.AlterColumn<string>(
                name: "MachineId",
                table: "DailyOperationBadOutputReasons",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "BadOutputId",
                table: "DailyOperationBadOutputReasons",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
