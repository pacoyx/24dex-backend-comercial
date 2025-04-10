public interface ICreateCategoryProdService
{
    Task<CreateCategoryProdResponseDto> CreateCategoryProdAsync(CreateCategoryProdDto categoryProd);
}