using MediatR;
using Microsoft.AspNetCore.Mvc;
public static class GetTicketsToCollectEndpoint
{
    public static void MapGetTicketsToCollect(this IEndpointRouteBuilder app)
    {
        app.MapPost("/toCollect", async ([FromBody] GetTicketsToCollect.Command command,IMediator _mediator) =>
        {
            var result = await _mediator.Send(command);
            return Results.Ok(result);
        });
    }
}