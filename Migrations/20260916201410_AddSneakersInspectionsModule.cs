using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _24dex_backend_comercial.Migrations
{
    /// <inheritdoc />
    public partial class AddSneakersInspectionsModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "WorkGuideMains",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ItemInspections",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkGuideDetailId = table.Column<int>(type: "int", nullable: false),
                    HasStains = table.Column<bool>(type: "bit", nullable: false),
                    HasTears = table.Column<bool>(type: "bit", nullable: false),
                    HasFading = table.Column<bool>(type: "bit", nullable: false),
                    HasMissingParts = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemInspections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemInspections_WorkGuideDetails_WorkGuideDetailId",
                        column: x => x.WorkGuideDetailId,
                        principalTable: "WorkGuideDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SneakerBrands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SneakerBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemPhotos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemInspectionId = table.Column<long>(type: "bigint", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: false),
                    OriginalFileName = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: true),
                    BlobUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPhotos_ItemInspections_ItemInspectionId",
                        column: x => x.ItemInspectionId,
                        principalTable: "ItemInspections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SneakerDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkGuideDetailId = table.Column<int>(type: "int", nullable: false),
                    SneakerBrandId = table.Column<int>(type: "int", nullable: true),
                    PrimaryColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SecondaryColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HasLaces = table.Column<bool>(type: "bit", nullable: false),
                    HasInsoles = table.Column<bool>(type: "bit", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SneakerDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SneakerDetails_SneakerBrands_SneakerBrandId",
                        column: x => x.SneakerBrandId,
                        principalTable: "SneakerBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SneakerDetails_WorkGuideDetails_WorkGuideDetailId",
                        column: x => x.WorkGuideDetailId,
                        principalTable: "WorkGuideDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SneakerBrands",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, true, "Nike" },
                    { 2, true, "Adidas" },
                    { 3, true, "Reebok" },
                    { 4, true, "Puma" },
                    { 5, true, "New Balance" },
                    { 6, true, "Converse" },
                    { 7, true, "Vans" },
                    { 8, true, "Asics" },
                    { 9, true, "Fila" },
                    { 10, true, "Under Armour" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkGuideMains_BranchId",
                table: "WorkGuideMains",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemInspections_WorkGuideDetailId",
                table: "ItemInspections",
                column: "WorkGuideDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemPhotos_ItemInspectionId",
                table: "ItemPhotos",
                column: "ItemInspectionId");

            migrationBuilder.CreateIndex(
                name: "IX_SneakerDetails_SneakerBrandId",
                table: "SneakerDetails",
                column: "SneakerBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_SneakerDetails_WorkGuideDetailId",
                table: "SneakerDetails",
                column: "WorkGuideDetailId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkGuideMains_BranchSales_BranchId",
                table: "WorkGuideMains",
                column: "BranchId",
                principalTable: "BranchSales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkGuideMains_BranchSales_BranchId",
                table: "WorkGuideMains");

            migrationBuilder.DropTable(
                name: "ItemPhotos");

            migrationBuilder.DropTable(
                name: "SneakerDetails");

            migrationBuilder.DropTable(
                name: "ItemInspections");

            migrationBuilder.DropTable(
                name: "SneakerBrands");

            migrationBuilder.DropIndex(
                name: "IX_WorkGuideMains_BranchId",
                table: "WorkGuideMains");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "WorkGuideMains");
        }
    }
}
