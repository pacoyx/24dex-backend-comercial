public record CreateCategoryProdDto
{    
    public string Name { get; set; } = string.Empty;    
    public string Description { get; set; } = string.Empty;    
    public string Status { get; set; } = "A";
}
public record CreateCategoryProdResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "A";
}