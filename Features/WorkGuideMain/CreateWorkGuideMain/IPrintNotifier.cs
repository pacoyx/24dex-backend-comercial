namespace _24dex_backend_comercial.Features.WorkGuideMain.CreateWorkGuideMain;

public interface IPrintNotifier
{
    Task NotifyAsync(
        int printerId,
        long printQueueId);
}