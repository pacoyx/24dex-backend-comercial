public static class ReportEndpoints
{
    public static void MapReport(this WebApplication app)
    {
        var group = app.MapGroup("/api/reports");
        group.MapWorkGuideByDate();
        group.MapWorkGuideByCustomers();
    }
}