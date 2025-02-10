using MediatR;
using Microsoft.AspNetCore.Mvc;
public static class GetCollectionWorkerByIdEndpoint
{
    public static void MapGetCollectionWorkerById(this IEndpointRouteBuilder app)
    {
        app.MapPost("/findById", async ([FromBody] GetClothingWorkerById.Command command, IMediator _mediator) =>
        {
            var result = await _mediator.Send(command);
            return Results.Ok(result.response);
        });
    }
}