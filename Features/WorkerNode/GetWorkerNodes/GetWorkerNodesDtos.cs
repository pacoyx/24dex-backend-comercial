public record WorkerNodeComboResponse(
    int Id,
    int BranchId,
    string WorkerKey,
    string ComputerName,
    bool IsActive,
    DateTime? LastConnectionAt
);
