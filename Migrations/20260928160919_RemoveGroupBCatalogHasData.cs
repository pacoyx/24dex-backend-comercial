using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _24dex_backend_comercial.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGroupBCatalogHasData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // No-op intencional: Areas, EmployeeDocumentTypes, ExpenseCategories, Positions y
            // SneakerBrands dejaron de declararse vía HasData porque ahora los administra
            // IApplicationSeeder (Data/Seeding). Las filas ya sembradas por migraciones previas
            // permanecen intactas en la base de datos; esta migración solo actualiza el snapshot
            // del modelo para que EF deje de detectar un diff fantasma.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Ver comentario en Up().
        }
    }
}
