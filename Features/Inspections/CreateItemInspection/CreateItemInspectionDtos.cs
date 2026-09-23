public record CreateItemInspectionRequest(
    bool HasStains,
    bool HasTears,
    bool HasFading,
    bool HasMissingParts,
    string? Notes
);
