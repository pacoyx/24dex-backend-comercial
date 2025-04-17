public interface ICreateSupplierService
{
    Task<CreateSupplierResponseDto> CreateSupplierAsync(CreateSupplierRequestDto request);
}
