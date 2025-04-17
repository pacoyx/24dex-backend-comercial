
public record CreateSupplierRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Ruc { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;    
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Status { get; set; } = "A";
    public string ContactName { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
}

public record CreateSupplierResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Ruc { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Status { get; set; } = "A";
    public string ContactName { get; set; } = string.Empty;
    public string ContactPhone { get; set; } = string.Empty;
}