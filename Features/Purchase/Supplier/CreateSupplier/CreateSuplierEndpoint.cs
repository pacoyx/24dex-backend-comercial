public static class CreateSupplierEndpoint
{
    public static RouteGroupBuilder MapCreateSupplier(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (ICreateSupplierService service, CreateSupplierRequestDto request) =>
        {
            var result = await service.CreateSupplierAsync(request);
            var response = new ApiResponse<CreateSupplierResponseDto>
            {
                Success = true,
                Data = result,
                StatusCode = StatusCodes.Status201Created,
                Message = "Supplier created successfully"
            };
            return  Results.Ok(response);
        })
        .WithName("Create Supplier")
        .WithTags("Supplier")
        .Produces<CreateSupplierResponseDto>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest);

        return group;
    }
}