using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _24dex_backend_comercial.Migrations
{
    /// <inheritdoc />
    public partial class AddCorporateOperationsModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerPriceLists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPriceLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerPriceLists_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPriceListDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerPriceListId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceItemId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPriceListDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerPriceListDetails_CustomerPriceLists_CustomerPriceListId",
                        column: x => x.CustomerPriceListId,
                        principalTable: "CustomerPriceLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerPriceListDetails_ProdServices_ServiceItemId",
                        column: x => x.ServiceItemId,
                        principalTable: "ProdServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PickupGuides",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuideNumber = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    BranchSalesId = table.Column<int>(type: "int", nullable: false),
                    PickupDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Observation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CustomerPriceListId = table.Column<long>(type: "bigint", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupGuides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PickupGuides_BranchSales_BranchSalesId",
                        column: x => x.BranchSalesId,
                        principalTable: "BranchSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PickupGuides_CustomerPriceLists_CustomerPriceListId",
                        column: x => x.CustomerPriceListId,
                        principalTable: "CustomerPriceLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PickupGuides_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PickupGuideDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PickupGuideId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Observation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupGuideDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PickupGuideDetails_PickupGuides_PickupGuideId",
                        column: x => x.PickupGuideId,
                        principalTable: "PickupGuides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PickupGuideDetails_ProdServices_ServiceItemId",
                        column: x => x.ServiceItemId,
                        principalTable: "ProdServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPriceListDetails_CustomerPriceListId_ServiceItemId",
                table: "CustomerPriceListDetails",
                columns: new[] { "CustomerPriceListId", "ServiceItemId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPriceListDetails_ServiceItemId",
                table: "CustomerPriceListDetails",
                column: "ServiceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPriceLists_CustomerId_ActiveOnly",
                table: "CustomerPriceLists",
                column: "CustomerId",
                unique: true,
                filter: "[IsActive] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPriceLists_CustomerId_Version",
                table: "CustomerPriceLists",
                columns: new[] { "CustomerId", "Version" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PickupGuideDetails_PickupGuideId",
                table: "PickupGuideDetails",
                column: "PickupGuideId");

            migrationBuilder.CreateIndex(
                name: "IX_PickupGuideDetails_ServiceItemId",
                table: "PickupGuideDetails",
                column: "ServiceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PickupGuides_BranchSalesId",
                table: "PickupGuides",
                column: "BranchSalesId");

            migrationBuilder.CreateIndex(
                name: "IX_PickupGuides_CustomerId_Status",
                table: "PickupGuides",
                columns: new[] { "CustomerId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_PickupGuides_CustomerPriceListId",
                table: "PickupGuides",
                column: "CustomerPriceListId");

            migrationBuilder.CreateIndex(
                name: "IX_PickupGuides_GuideNumber",
                table: "PickupGuides",
                column: "GuideNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerPriceListDetails");

            migrationBuilder.DropTable(
                name: "PickupGuideDetails");

            migrationBuilder.DropTable(
                name: "PickupGuides");

            migrationBuilder.DropTable(
                name: "CustomerPriceLists");
        }
    }
}
