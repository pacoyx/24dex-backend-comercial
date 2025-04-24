public class UpdateProductService : IUpdateProductService
{
    private readonly RecepcionDbContext _context;
    public IAppLogger<UpdateProductService> Logger { get; }

    public UpdateProductService(RecepcionDbContext context, IAppLogger<UpdateProductService> logger)
    {
        _context = context;
        Logger = logger;
    }

    public async Task<string> UpdateProductAsync(UpdateProductRequestDto request)
    {
        var product = await _context.Products.FindAsync(request.Id);
        if (product == null)
        {
            Logger.LogWarning($"Product with id {request.Id} not found.");
            return "Product not found";
        }

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Stock = request.Stock;
        product.UnitMeasurementId = request.UnitMeasurementId;
        product.CategoryProdId = request.CategoryProdId;
        product.Status = request.Status;

        await _context.SaveChangesAsync();
        return "OK";
    }
}