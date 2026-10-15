using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _24dex_backend_comercial.Migrations
{
    /// <inheritdoc />
    public partial class AddContentTypeAndFileSizeToItemPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "ItemPhotos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                table: "ItemPhotos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "ItemPhotos");

            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "ItemPhotos");
        }
    }
}
