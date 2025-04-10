public interface ICreateProductService
{
    Task<CreateProductResponseDto> CreateProductAsync(CreateProductRequestDto request);
}

