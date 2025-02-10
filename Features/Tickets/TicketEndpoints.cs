public static class TicketEndpoints
{
    public static void MapTicket(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/ticket");
        group.MapCreateTicket();        
        group.MapGetTicketsToCollect();
        // group.MapGetTicket();
        // group.MapGetTickets();
        // group.MapUpdateTicket();
        // group.MapDeleteTicket();
        // group.MapSearchTicket();
        // group.MapSearchTicketPaginator();
    }
}