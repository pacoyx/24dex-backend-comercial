using Microsoft.EntityFrameworkCore;

public class GetProductsService : IGetProductsService
{

    private readonly RecepcionDbContext _context;
    public IAppLogger<GetServicesAccessFastService> Logger { get; }

    public GetProductsService(RecepcionDbContext context, IAppLogger<GetServicesAccessFastService> logger)
    {
        _context = context;
        Logger = logger;
    }

    public async Task<GetProductsResponseDto?> GetProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            Logger.LogWarning($"Product with id {id} not found.");
            return null;
        }

        return new GetProductsResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            UnitMeasurementId = product.UnitMeasurementId,
            CategoryProdId = product.CategoryProdId,
            Status = product.Status
        };
    }

    public async Task<IEnumerable<GetProductsResponseDto>> GetProductsAsync()
    {
        var products = await _context.Products.ToListAsync();
        if (products == null || !products.Any())
        {
            Logger.LogWarning("No products found.");
            return Enumerable.Empty<GetProductsResponseDto>();
        }

        return products.Select(product => new GetProductsResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            UnitMeasurementId = product.UnitMeasurementId,
            CategoryProdId = product.CategoryProdId,
            Status = product.Status
        }).ToList();
    }

    async public Task<GetProductsResponsePaginatorDto> GetProductsPaginatorAsync(int pageNumber, int pageSize, string name)
    {

        var query = _context.Products.AsNoTracking();
        var totalRows = await query.CountAsync();

        if (name != null && !string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(x => x.Name.Contains(name));
        }

        var products = await query
            .OrderBy(p => p.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        if (products == null || !products.Any())
        {
            Logger.LogWarning("No products found for the given page.");
            return new GetProductsResponsePaginatorDto(0, new List<GetProductsResponseDto>());
        }

        var responsePaginator = new GetProductsResponsePaginatorDto(
            totalRows,
            products.Select(p => new GetProductsResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                UnitMeasurementId = p.UnitMeasurementId,
                CategoryProdId = p.CategoryProdId,
                Status = p.Status
            }).ToList()
        );

        return responsePaginator;

    }
}