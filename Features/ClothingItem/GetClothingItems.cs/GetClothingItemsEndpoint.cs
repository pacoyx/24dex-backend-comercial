using MediatR;
using Microsoft.AspNetCore.Mvc;
public static class GetClothingItemsEndpoint
{
    public static void MapGetClothingItems(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async ([FromBody] GetClothingItems.Command command, IMediator _mediator) =>
        {
            var result = await _mediator.Send(command);
            return Results.Ok(result.response);
        });
    }
}