using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _24dex_backend_comercial.Migrations
{
    /// <inheritdoc />
    public partial class addcolspagorecojoestado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EstadoRegistro",
                table: "WorkGuideMains",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EstadoSituacion",
                table: "WorkGuideMains",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPago",
                table: "WorkGuideMains",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRecojo",
                table: "WorkGuideMains",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoRegistro",
                table: "WorkGuideMains");

            migrationBuilder.DropColumn(
                name: "EstadoSituacion",
                table: "WorkGuideMains");

            migrationBuilder.DropColumn(
                name: "FechaPago",
                table: "WorkGuideMains");

            migrationBuilder.DropColumn(
                name: "FechaRecojo",
                table: "WorkGuideMains");
        }
    }
}
