public interface IGetProductsService
{
    Task<GetProductsResponseDto?> GetProductAsync(int id);
    Task<IEnumerable<GetProductsResponseDto>> GetProductsAsync();
    Task<GetProductsResponsePaginatorDto> GetProductsPaginatorAsync(int pageNumber, int pageSize, string name);
 
}