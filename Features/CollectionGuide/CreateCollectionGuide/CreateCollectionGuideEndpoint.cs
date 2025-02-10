using MediatR;
using Microsoft.AspNetCore.Mvc;
public static class CreateCollectionGuideEndpoint
{
    public static void MapCreateCollectionGuide(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async ([FromBody] CreateCollectionGuide.Command command, IMediator _mediator) =>
        {
            var result = await _mediator.Send(command);
            return Results.Ok(result);
        });
    }
}