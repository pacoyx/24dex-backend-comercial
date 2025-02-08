using MediatR;
using Microsoft.AspNetCore.Mvc;
public static class CreateTicketEndpoint
{
    public static void MapCreateTicket(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async ([FromBody] CreateTicket.Command command, IMediator _mediator) =>
        {        
            var result = await _mediator.Send(command);
            return Results.Ok(result);
        });
    }
}