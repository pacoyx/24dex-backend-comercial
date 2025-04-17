
public class CreateSupplierService : ICreateSupplierService
{
    private readonly RecepcionDbContext _context;
    public ILogger<CreateSupplierService> Logger { get; }

    public CreateSupplierService(RecepcionDbContext context, ILogger<CreateSupplierService> logger)
    {
        _context = context;
        Logger = logger;
    }

    public Task<CreateSupplierResponseDto> CreateSupplierAsync(CreateSupplierRequestDto request)
    {
        var supplier = new Supplier
        {
            Name = request.Name,
            Ruc = request.Ruc,
            Address = request.Address,
            Phone = request.Phone,
            Email = request.Email,
            Status = request.Status,
            ContactName = request.ContactName,
            ContactPhone = request.ContactPhone
        };

        _context.Suppliers.Add(supplier);
        _context.SaveChanges();

        Logger.LogInformation($"Supplier {supplier.Name} created successfully.");

        return Task.FromResult(new CreateSupplierResponseDto
        {
            Id = supplier.Id,
            Name = supplier.Name,
            Ruc = supplier.Ruc,
            Address = supplier.Address,
            Phone = supplier.Phone,
            Email = supplier.Email,
            Status = supplier.Status,
            ContactName = supplier.ContactName,
            ContactPhone = supplier.ContactPhone
        });
    }
}