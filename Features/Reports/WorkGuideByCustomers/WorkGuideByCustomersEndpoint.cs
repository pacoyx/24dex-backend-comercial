using Microsoft.EntityFrameworkCore;

public static class WorkGuideByCustomersEndpoint
{
    public static void MapWorkGuideByCustomers(this IEndpointRouteBuilder app)
    {
        app.MapGet("/workguides/bycustomer/{customerId}/{pageNumber}/{pageSize}", async (int customerId, int pageNumber, int pageSize, RecepcionDbContext dbContext) =>
        {

            var query = dbContext.WorkGuideMains
            .AsNoTracking()
            .Include(wg => wg.Customer)
            .Include(wg => wg.WorkGuideDetails)
            .ThenInclude(wgd => wgd.Product)
            .Where(wg => wg.CustomerId == customerId);

            var totalRows = await query.CountAsync();


            var workGuides = await query
                .OrderByDescending(c => c.FechaOperacion)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var responseDto = new GetWgsByCustomersPaginatorResponseDto(
                totalRows,
                workGuides.Select(wg => new WgByCustomersResponseDto(
                    wg.Id,
                    wg.SerieGuia,
                    wg.NumeroGuia,
                    wg.FechaOperacion,
                    wg.FechaHoraEntrega,
                    wg.Observaciones,
                    wg.TipoPago,
                    wg.DescripcionPago,
                    wg.TipoRecepcion,
                    wg.Total,
                    wg.Acuenta,
                    wg.Saldo,
                    wg.Customer != null ? wg.Customer.FirtsName + " " + wg.Customer.LastName : string.Empty,
                    wg.Customer?.Phone ?? string.Empty,
                    wg.EstadoPago,
                    wg.FechaPago,
                    wg.EstadoRegistro,
                    wg.EstadoSituacion,
                    wg.FechaRecojo,
                    wg.TipoPagoCancelacion,
                    wg.WorkGuideDetails.Select(wgd => new WgByCustomersDetailResponseDto
                    {
                        Id = wgd.Id,
                        Cant = wgd.Cant,
                        Precio = wgd.Precio,
                        Total = wgd.Total,
                        Servicio = wgd.Product?.Name ?? string.Empty,
                        Observaciones = wgd.Observaciones,
                        Ubicacion = wgd.Ubicacion,
                        EstadoTrabajo = wgd.EstadoTrabajo,
                        EstadoRegistro = wgd.EstadoRegistro,
                        EstadoSituacion = wgd.EstadoSituacion,
                        EstadoPago = wgd.EstadoPago,
                        FechaRecojo = wgd.FechaRecojo,
                        FechaDevolucion = wgd.FechaDevolucion
                    }).ToArray()
                ))
            );

            var response = new ApiResponse<GetWgsByCustomersPaginatorResponseDto>
            {
                Data = responseDto,
                Success = true,
                Message = "",
                StatusCode = 200
            };

            return Results.Ok(response);
        })
        .WithName("GetWorkGuidesByCustomers");
    }
}