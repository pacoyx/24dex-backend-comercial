using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace _24dex_backend_comercial.Features.PrinterHub;
public class PrintHub : Hub
{

    private readonly RecepcionDbContext _db;
    private readonly PrinterRegistry _registry;
    private readonly ILogger<PrintHub> _logger;

    public PrintHub(
        RecepcionDbContext db,
        PrinterRegistry registry,
        ILogger<PrintHub> logger)
    {
        _db = db;
        _registry = registry;
        _logger = logger;
    }

    public async Task RegisterPrinter(
        string workerKey, string computerName)
    {
        _logger.LogInformation(
            "Intentando registrar WorkerKey {WorkerKey}",
            workerKey);

        var worker = await _db.WorkerNodes
            .Include(x => x.Branch)
            .SingleOrDefaultAsync(x =>
                x.WorkerKey == workerKey &&
                x.IsActive);
        if (worker is null)
        {
            _logger.LogWarning(
                "WorkerKey {WorkerKey} no encontrado",
                workerKey);
            throw new HubException(
                $"WorkerKey '{workerKey}' no registrado.");
        }
        
        worker.ComputerName = computerName;
        worker.LastConnectionAt = DateTime.UtcNow;
        
        await _db.SaveChangesAsync();
        await Groups.AddToGroupAsync(Context.ConnectionId, $"worker-{worker.Id}");
        await Groups.AddToGroupAsync(Context.ConnectionId, $"branch-{worker.BranchId}");
        
        _registry.Register(
            worker.Id,
            worker.BranchId,
            Context.ConnectionId);
        
        _logger.LogInformation(
            "WorkerNode {WorkerNodeId} registrado para sucursal {BranchId}",
            worker.Id,
            worker.BranchId);
    }
    
    public Task Heartbeat()
    {
        _registry.Heartbeat(Context.ConnectionId);
        return Task.CompletedTask;
    }
    
    public override async Task OnDisconnectedAsync(
        Exception? exception)
    {
        _registry.Remove(Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }
    
    
}