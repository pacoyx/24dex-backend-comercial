public static class ProductEndpoints
{
    public static RouteGroupBuilder MapProduct(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/product");

        group.MapGetProducts();
        group.MapCreateProduct();
        // group.MapUpdateProduct();
        // group.MapDeleteProduct();

        return group;
    }
}