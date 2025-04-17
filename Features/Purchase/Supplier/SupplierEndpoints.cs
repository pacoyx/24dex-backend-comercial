    public static class SupplierEndpoints
    {
        public static RouteGroupBuilder MapSupplier(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/supplier");

            group.MapGetSupplier();
            group.MapCreateSupplier();
            group.MapUpdateSupplier();
            // group.MapDeleteSupplier();

            return group;
        }
    }