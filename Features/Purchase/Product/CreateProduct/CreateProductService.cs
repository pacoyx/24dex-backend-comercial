
public class CreateProductService : ICreateProductService
{
   private readonly RecepcionDbContext _context;
    public IAppLogger<GetServicesAccessFastService> Logger { get; }

    public CreateProductService(RecepcionDbContext context, IAppLogger<GetServicesAccessFastService> logger)
    {
        _context = context;
        Logger = logger;
    }
    
    public Task<CreateProductResponseDto> CreateProductAsync(CreateProductRequestDto request)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock,
            UnitMeasurementId = request.UnitMeasurementId,
            CategoryProdId = request.CategoryProdId,
            Status = request.Status
        };

        _context.Products.Add(product);
        _context.SaveChanges();

        return Task.FromResult(new CreateProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            UnitMeasurementId = product.UnitMeasurementId,
            CategoryProdId = product.CategoryProdId,
            Status = product.Status
        });
    }
}