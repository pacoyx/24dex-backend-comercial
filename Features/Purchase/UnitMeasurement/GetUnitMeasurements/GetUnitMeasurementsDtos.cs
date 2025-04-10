public record GetUnitMeasurementsResponseDto
{
    public int Id { get; set; }
    public string CodeUm { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "A";
}

public record GetUnitMeasurementsComboResponseDto
{
    public int Id { get; set; }
    public string CodeUm { get; set; } = string.Empty;    
}