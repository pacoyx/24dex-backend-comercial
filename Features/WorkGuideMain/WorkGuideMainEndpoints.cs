public static class WorkGuideMainEndpoints{
    public static void MapWorkGuideMain(this WebApplication app){
        var group = app.MapGroup("/api/workGuideMain");
        group.MapCreateWorkGuideMain();
        group.MapGetWorkGuideMain();
        group.MapGetWorkGuidesMain();
        group.MapDeleteWorkGuideMain();        
    }
}