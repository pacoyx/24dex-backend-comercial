using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _24dex_backend_comercial.Migrations
{
    /// <inheritdoc />
    public partial class ChangeItemPhotoRelationToWorkGuideMain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPhotos_ItemInspections_ItemInspectionId",
                table: "ItemPhotos");

            migrationBuilder.DropIndex(
                name: "IX_ItemPhotos_ItemInspectionId",
                table: "ItemPhotos");

            migrationBuilder.DropColumn(
                name: "ItemInspectionId",
                table: "ItemPhotos");

            migrationBuilder.AddColumn<int>(
                name: "WorkGuideMainId",
                table: "ItemPhotos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ItemPhotos_WorkGuideMainId",
                table: "ItemPhotos",
                column: "WorkGuideMainId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPhotos_WorkGuideMains_WorkGuideMainId",
                table: "ItemPhotos",
                column: "WorkGuideMainId",
                principalTable: "WorkGuideMains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPhotos_WorkGuideMains_WorkGuideMainId",
                table: "ItemPhotos");

            migrationBuilder.DropIndex(
                name: "IX_ItemPhotos_WorkGuideMainId",
                table: "ItemPhotos");

            migrationBuilder.DropColumn(
                name: "WorkGuideMainId",
                table: "ItemPhotos");

            migrationBuilder.AddColumn<long>(
                name: "ItemInspectionId",
                table: "ItemPhotos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ItemPhotos_ItemInspectionId",
                table: "ItemPhotos",
                column: "ItemInspectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPhotos_ItemInspections_ItemInspectionId",
                table: "ItemPhotos",
                column: "ItemInspectionId",
                principalTable: "ItemInspections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
