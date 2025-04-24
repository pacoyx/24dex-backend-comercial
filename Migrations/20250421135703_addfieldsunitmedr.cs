using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _24dex_backend_comercial.Migrations
{
    /// <inheritdoc />
    public partial class addfieldsunitmedr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                table: "UnitMeasurements",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CodeSunat",
                table: "UnitMeasurements",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "UnitMeasurements",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "PurchaseInvoices",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurrencyId",
                table: "PurchaseInvoices",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ExchangeRate",
                table: "PurchaseInvoices",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Igv",
                table: "PurchaseInvoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Subtotal",
                table: "PurchaseInvoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abbreviation",
                table: "UnitMeasurements");

            migrationBuilder.DropColumn(
                name: "CodeSunat",
                table: "UnitMeasurements");

            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "UnitMeasurements");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "PurchaseInvoices");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "PurchaseInvoices");

            migrationBuilder.DropColumn(
                name: "ExchangeRate",
                table: "PurchaseInvoices");

            migrationBuilder.DropColumn(
                name: "Igv",
                table: "PurchaseInvoices");

            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "PurchaseInvoices");
        }
    }
}
