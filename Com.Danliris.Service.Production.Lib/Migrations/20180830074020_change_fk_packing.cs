using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class change_fk_packing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packings_PackingDetails_PackingDetailId",
                table: "Packings");

            migrationBuilder.DropIndex(
                name: "IX_Packings_PackingDetailId",
                table: "Packings");

            migrationBuilder.DropColumn(
                name: "PackingDetailId",
                table: "Packings");

            migrationBuilder.AddColumn<int>(
                name: "PackingId",
                table: "PackingDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PackingDetails_PackingId",
                table: "PackingDetails",
                column: "PackingId");

            migrationBuilder.AddForeignKey(
                name: "FK_PackingDetails_Packings_PackingId",
                table: "PackingDetails",
                column: "PackingId",
                principalTable: "Packings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackingDetails_Packings_PackingId",
                table: "PackingDetails");

            migrationBuilder.DropIndex(
                name: "IX_PackingDetails_PackingId",
                table: "PackingDetails");

            migrationBuilder.DropColumn(
                name: "PackingId",
                table: "PackingDetails");

            migrationBuilder.AddColumn<int>(
                name: "PackingDetailId",
                table: "Packings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Packings_PackingDetailId",
                table: "Packings",
                column: "PackingDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packings_PackingDetails_PackingDetailId",
                table: "Packings",
                column: "PackingDetailId",
                principalTable: "PackingDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
