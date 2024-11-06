using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _24dex_backend_comercial.Migrations
{
    /// <inheritdoc />
    public partial class addalertas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlertMsgId",
                table: "WorkGuideMains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AlertMsgId1",
                table: "WorkGuideMains",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AlertMsgs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoAlerta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkGuideMainId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertMsgs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkGuideMains_AlertMsgId1",
                table: "WorkGuideMains",
                column: "AlertMsgId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkGuideMains_AlertMsgs_AlertMsgId1",
                table: "WorkGuideMains",
                column: "AlertMsgId1",
                principalTable: "AlertMsgs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkGuideMains_AlertMsgs_AlertMsgId1",
                table: "WorkGuideMains");

            migrationBuilder.DropTable(
                name: "AlertMsgs");

            migrationBuilder.DropIndex(
                name: "IX_WorkGuideMains_AlertMsgId1",
                table: "WorkGuideMains");

            migrationBuilder.DropColumn(
                name: "AlertMsgId",
                table: "WorkGuideMains");

            migrationBuilder.DropColumn(
                name: "AlertMsgId1",
                table: "WorkGuideMains");
        }
    }
}
