public interface IGetProductsService
{
    Task<GetProductsResponseDto?> GetProductAsync(int id);
    Task<IEnumerable<GetProductsResponseDto>> GetProductsAsync();
 
}