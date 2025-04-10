public record GetCategoriesProdResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Status { get; init; } = "A"; // e.g., "Active", "Inactive"
}

public record GetCategoriesProdShortResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;        
}