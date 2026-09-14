public record UpdateWorkerNodeDto(
    int BranchId,
    string WorkerKey,
    string ComputerName,
    bool IsActive
);
