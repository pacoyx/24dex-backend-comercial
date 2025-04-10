public static class CategoryProdEndpoints{
    public static RouteGroupBuilder MapCategoryProd(this IEndpointRouteBuilder app){
        var group = app.MapGroup("/api/categoryProd");

        group.MapCreateCategoryProd();
        group.MapGetCategoriesProd();
        group.MapGetCategoriesProdShort();
        // group.MapGetCategoryProd();
        // group.MapDeleteCategoryProd();
        // group.MapUpdateCategoryProd();
        // group.MapGetCategoryProdByCat();
        // group.MapGetCategoryProdSearchByDescription();

        return group;
    }
}