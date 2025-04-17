
using Microsoft.EntityFrameworkCore;

public class UpdateSupplierService : IUpdateSupplierService
{
 private readonly RecepcionDbContext _context;
    public ILogger<CreateSupplierService> Logger { get; }

    public UpdateSupplierService(RecepcionDbContext context, ILogger<CreateSupplierService> logger)
    {
        _context = context;
        Logger = logger;
    }

    async public Task<string> UpdateSupplierAsync(UpdateSupplierRequest request)
    {
        var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.Id == request.Id);
        if (supplier == null)
        {
            Logger.LogError($"Supplier with ID {request.Id} not found.");
            return $"Supplier with ID {request.Id} not found.";
        }

        supplier.Name = request.Name;
        supplier.Ruc = request.Ruc;
        supplier.Address = request.Address;
        supplier.Phone = request.Phone;
        supplier.Email = request.Email;
        supplier.Status = request.Status;
        supplier.ContactName = request.ContactName;
        supplier.ContactPhone = request.ContactPhone;

        _context.SaveChanges();

        Logger.LogInformation($"Supplier {supplier.Name} updated successfully.");

        return "OK";
    }
}