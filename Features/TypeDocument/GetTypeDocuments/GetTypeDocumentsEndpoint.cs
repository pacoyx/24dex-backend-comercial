using Microsoft.EntityFrameworkCore;

public static class GetTypeDocumentsEndpoint
{
    public static void MapGetTypeDocuments(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (RecepcionDbContext db) =>
        {
            var typeDocuments = await db.TypeDocuments
                .AsNoTracking()
                .Where(x => x.Status == "A")
                .OrderBy(x => x.Name)
                .Select(x => new TypeDocumentResponse(x.Id, x.Name, x.Description, x.DocId, x.Status))
                .ToListAsync();

            return Results.Ok(new ApiResponse<List<TypeDocumentResponse>>
            {
                Success = true,
                Message = "Listado de tipos de documento.",
                StatusCode = StatusCodes.Status200OK,
                Data = typeDocuments
            });
        });
    }
}
