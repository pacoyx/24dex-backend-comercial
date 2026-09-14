using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _24dex_backend_comercial.Migrations
{
    /// <inheritdoc />
    public partial class RestructurePrinterWorkerNode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Printers_BranchSales_BranchId",
                table: "Printers");

            migrationBuilder.DropColumn(
                name: "ComputerName",
                table: "Printers");

            migrationBuilder.DropColumn(
                name: "ConnectionKey",
                table: "Printers");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Printers",
                newName: "WindowsPrinterName");

            migrationBuilder.RenameColumn(
                name: "LastConnectionAt",
                table: "Printers",
                newName: "LastDiscoveredAt");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "Printers",
                newName: "WorkerNodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Printers_BranchId",
                table: "Printers",
                newName: "IX_Printers_WorkerNodeId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Printers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "WorkerNodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    WorkerKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComputerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastConnectionAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerNodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkerNodes_BranchSales_BranchId",
                        column: x => x.BranchId,
                        principalTable: "BranchSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkerNodes_BranchId",
                table: "WorkerNodes",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Printers_WorkerNodes_WorkerNodeId",
                table: "Printers",
                column: "WorkerNodeId",
                principalTable: "WorkerNodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Printers_WorkerNodes_WorkerNodeId",
                table: "Printers");

            migrationBuilder.DropTable(
                name: "WorkerNodes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Printers");

            migrationBuilder.RenameColumn(
                name: "WorkerNodeId",
                table: "Printers",
                newName: "BranchId");

            migrationBuilder.RenameColumn(
                name: "WindowsPrinterName",
                table: "Printers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LastDiscoveredAt",
                table: "Printers",
                newName: "LastConnectionAt");

            migrationBuilder.RenameIndex(
                name: "IX_Printers_WorkerNodeId",
                table: "Printers",
                newName: "IX_Printers_BranchId");

            migrationBuilder.AddColumn<string>(
                name: "ComputerName",
                table: "Printers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConnectionKey",
                table: "Printers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Printers_BranchSales_BranchId",
                table: "Printers",
                column: "BranchId",
                principalTable: "BranchSales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
