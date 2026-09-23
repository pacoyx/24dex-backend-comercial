public record UpsertSneakerDetailRequest(
    int? BrandId,
    string? PrimaryColor,
    string? SecondaryColor,
    bool HasLaces,
    bool HasInsoles,
    string? Observations
);
