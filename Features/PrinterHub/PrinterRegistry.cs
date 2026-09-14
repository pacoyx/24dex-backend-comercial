using System.Collections.Concurrent;

namespace _24dex_backend_comercial.Features.PrinterHub;

public sealed class ConnectedWorker
{
    public int WorkerNodeId { get; set; }
    public int BranchId { get; set; }
    public string ConnectionId { get; set; } = string.Empty;
    public DateTime ConnectedAt { get; set; }
    public DateTime LastHeartbeat { get; set; }
}


public sealed class PrinterRegistry
{
    private readonly ConcurrentDictionary<string, ConnectedWorker>
        _connections = new();

    public void Register(
        int workerNodeId,
        int branchId,
        string connectionId)
    {
        _connections[connectionId] =
            new ConnectedWorker
            {
                WorkerNodeId = workerNodeId,
                BranchId = branchId,
                ConnectionId = connectionId,
                ConnectedAt = DateTime.UtcNow,
                LastHeartbeat = DateTime.UtcNow
            };
    }

    public void Heartbeat(
        string connectionId)
    {
        if (_connections.TryGetValue(
                connectionId,
                out var worker))
        {
            worker.LastHeartbeat =
                DateTime.UtcNow;
        }
    }

    public void Remove(
        string connectionId)
    {
        _connections.TryRemove(
            connectionId,
            out _);
    }

    public IReadOnlyCollection<ConnectedWorker> GetAll()
    {
        return _connections.Values.ToList();
    }

    public ConnectedWorker? GetByConnectionId(
        string connectionId)
    {
        _connections.TryGetValue(
            connectionId,
            out var worker);

        return worker;
    }

    public ConnectedWorker? GetByWorkerNodeId(
        int workerNodeId)
    {
        return _connections.Values
            .FirstOrDefault(x =>
                x.WorkerNodeId == workerNodeId);
    }

    public IEnumerable<ConnectedWorker>
        GetByBranchId(
            int branchId)
    {
        return _connections.Values
            .Where(x =>
                x.BranchId == branchId);
    }

    public bool IsConnected(
        int workerNodeId)
    {
        return _connections.Values
            .Any(x =>
                x.WorkerNodeId == workerNodeId);
    }

    public int Count =>
        _connections.Count;
}
