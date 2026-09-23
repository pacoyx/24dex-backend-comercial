using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _24dex_backend_comercial.Migrations
{
    /// <inheritdoc />
    public partial class AddPrintCopyToPrintQueue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "WorkGuideMains",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrintCopy",
                table: "PrintQueues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WorkGuideMains_UserId",
                table: "WorkGuideMains",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkGuideMains_Users_UserId",
                table: "WorkGuideMains",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkGuideMains_Users_UserId",
                table: "WorkGuideMains");

            migrationBuilder.DropIndex(
                name: "IX_WorkGuideMains_UserId",
                table: "WorkGuideMains");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "WorkGuideMains");

            migrationBuilder.DropColumn(
                name: "PrintCopy",
                table: "PrintQueues");
        }
    }
}
