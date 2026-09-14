using Microsoft.EntityFrameworkCore;

public static class GetPrintersEndpoint
{
    public static void MapGetPrinters(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext context) =>
        {
            var printers = await context.Printers.AsNoTracking().ToListAsync();

            var response = new ApiResponse<List<PrinterComboResponse>>
            {
                Data = printers.Select(p => new PrinterComboResponse
                (
                    p.Id,
                    p.WorkerNodeId,
                    p.WindowsPrinterName,
                    p.IsActive,
                    p.LastDiscoveredAt
                )).ToList(),
                Message = "Lista de impresoras",
                StatusCode = 200,
                Success = true
            };

            return Results.Ok(response);
        });
    }
}
