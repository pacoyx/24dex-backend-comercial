using Microsoft.EntityFrameworkCore;

public static class GetIdServicePesoEndpoint
{
    public static RouteHandlerBuilder MapGetIdServicePeso(this IEndpointRouteBuilder app)
    {
        return app.MapGet("/getidservicepesolavado", async (RecepcionDbContext context) =>
          {
              var prodservice = await context.ProdServices
                  .FirstOrDefaultAsync(x => x.IsPeso == "S");
              var prodserviceLavado = await context.ProdServices
              .FirstOrDefaultAsync(x => x.IsLavado == "S");

              if (prodservice == null)
              {
                  return Results.NotFound("No se encontro el servicio para Peso KG.");
              }

              if (prodserviceLavado == null)
              {
                  return Results.NotFound("No se encontro el servicio para Lavado.");
              }

              var response = new ApiResponse<GetIdPesoLavadoResponseDto>
              {
                  Success = true,
                  Data = new GetIdPesoLavadoResponseDto
                  {
                      IdPeso = prodservice.Id,
                      IdLavado = prodserviceLavado.Id
                  },
                  StatusCode = 200,
                  Errors = [],
                  Message = "",
              };

              return Results.Ok(response);
          });
    }
}