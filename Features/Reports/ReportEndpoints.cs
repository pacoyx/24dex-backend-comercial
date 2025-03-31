public static class ReportEndpoints
{
    public static void MapReport(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/reports");
        group.MapWorkGuideByDate();
        group.MapWorkGuideByCustomers();
        group.MapDashboardCash();
    }
}