public record CreatePositionRequestDto
{
    public string Name { get; set; } = string.Empty;
}

public record CreatePositionResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
