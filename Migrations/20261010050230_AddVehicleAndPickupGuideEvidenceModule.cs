using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _24dex_backend_comercial.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleAndPickupGuideEvidenceModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmailDeliveryStatus",
                table: "PickupGuides",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "EmailRetryCount",
                table: "PickupGuides",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailSentAt",
                table: "PickupGuides",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                table: "PickupGuides",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEmailAttemptAt",
                table: "PickupGuides",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastEmailError",
                table: "PickupGuides",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "PickupGuides",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReceiveInvoices",
                table: "CustomerContacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ReceivePickupGuideEmails",
                table: "CustomerContacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ReceivePortalNotifications",
                table: "CustomerContacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PickupGuideAttachments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PickupGuideId = table.Column<long>(type: "bigint", nullable: false),
                    AttachmentType = table.Column<int>(type: "int", nullable: false),
                    OriginalFileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StoredFileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupGuideAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PickupGuideAttachments_PickupGuides_PickupGuideId",
                        column: x => x.PickupGuideId,
                        principalTable: "PickupGuides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PickupGuideRevisions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PickupGuideId = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    Observation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RevisionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupGuideRevisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PickupGuideRevisions_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PickupGuideRevisions_PickupGuides_PickupGuideId",
                        column: x => x.PickupGuideId,
                        principalTable: "PickupGuides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Plate = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PickupGuideDetailRevisions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PickupGuideRevisionId = table.Column<long>(type: "bigint", nullable: false),
                    PickupGuideDetailId = table.Column<long>(type: "bigint", nullable: false),
                    OriginalQuantity = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NewQuantity = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupGuideDetailRevisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PickupGuideDetailRevisions_PickupGuideDetails_PickupGuideDetailId",
                        column: x => x.PickupGuideDetailId,
                        principalTable: "PickupGuideDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PickupGuideDetailRevisions_PickupGuideRevisions_PickupGuideRevisionId",
                        column: x => x.PickupGuideRevisionId,
                        principalTable: "PickupGuideRevisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PickupGuides_EmployeeId",
                table: "PickupGuides",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PickupGuides_VehicleId",
                table: "PickupGuides",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_PickupGuideAttachments_PickupGuideId_AttachmentType_IsCurrent",
                table: "PickupGuideAttachments",
                columns: new[] { "PickupGuideId", "AttachmentType", "IsCurrent" });

            migrationBuilder.CreateIndex(
                name: "IX_PickupGuideDetailRevisions_PickupGuideDetailId",
                table: "PickupGuideDetailRevisions",
                column: "PickupGuideDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PickupGuideDetailRevisions_PickupGuideRevisionId",
                table: "PickupGuideDetailRevisions",
                column: "PickupGuideRevisionId");

            migrationBuilder.CreateIndex(
                name: "IX_PickupGuideRevisions_EmployeeId",
                table: "PickupGuideRevisions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PickupGuideRevisions_PickupGuideId",
                table: "PickupGuideRevisions",
                column: "PickupGuideId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Plate",
                table: "Vehicles",
                column: "Plate",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PickupGuides_Employees_EmployeeId",
                table: "PickupGuides",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PickupGuides_Vehicles_VehicleId",
                table: "PickupGuides",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PickupGuides_Employees_EmployeeId",
                table: "PickupGuides");

            migrationBuilder.DropForeignKey(
                name: "FK_PickupGuides_Vehicles_VehicleId",
                table: "PickupGuides");

            migrationBuilder.DropTable(
                name: "PickupGuideAttachments");

            migrationBuilder.DropTable(
                name: "PickupGuideDetailRevisions");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "PickupGuideRevisions");

            migrationBuilder.DropIndex(
                name: "IX_PickupGuides_EmployeeId",
                table: "PickupGuides");

            migrationBuilder.DropIndex(
                name: "IX_PickupGuides_VehicleId",
                table: "PickupGuides");

            migrationBuilder.DropColumn(
                name: "EmailDeliveryStatus",
                table: "PickupGuides");

            migrationBuilder.DropColumn(
                name: "EmailRetryCount",
                table: "PickupGuides");

            migrationBuilder.DropColumn(
                name: "EmailSentAt",
                table: "PickupGuides");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "PickupGuides");

            migrationBuilder.DropColumn(
                name: "LastEmailAttemptAt",
                table: "PickupGuides");

            migrationBuilder.DropColumn(
                name: "LastEmailError",
                table: "PickupGuides");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "PickupGuides");

            migrationBuilder.DropColumn(
                name: "ReceiveInvoices",
                table: "CustomerContacts");

            migrationBuilder.DropColumn(
                name: "ReceivePickupGuideEmails",
                table: "CustomerContacts");

            migrationBuilder.DropColumn(
                name: "ReceivePortalNotifications",
                table: "CustomerContacts");
        }
    }
}
