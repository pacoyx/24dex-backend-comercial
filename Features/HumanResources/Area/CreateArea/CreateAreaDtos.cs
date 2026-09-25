public record CreateAreaRequestDto
{
    public string Name { get; set; } = string.Empty;
}

public record CreateAreaResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
