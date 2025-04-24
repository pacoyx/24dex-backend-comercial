public interface IUpdateProductService{
    Task<string> UpdateProductAsync(UpdateProductRequestDto request);
}