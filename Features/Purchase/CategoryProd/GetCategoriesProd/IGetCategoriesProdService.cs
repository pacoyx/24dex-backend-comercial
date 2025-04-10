public interface IGetCategoriesProdService
{
    Task<List<GetCategoriesProdResponse>> GetCategoriesProdAsync();
    Task<List<GetCategoriesProdShortResponse>> GetCategoriesProdShortAsync();
    Task<CategoryProd> GetCategoryProdByIdAsync(int id);
}