public record GetProductsResponsePaginatorDto(
    int TotalCount,
    List<GetProductsResponseDto> Products
    );

public record GetProductsResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int UnitMeasurementId { get; set; }
    public int CategoryProdId { get; set; }
    public string Status { get; set; } = "A";
}
