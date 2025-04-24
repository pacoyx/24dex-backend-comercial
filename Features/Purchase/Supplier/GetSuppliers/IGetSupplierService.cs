public interface IGetSupplierService
{
    Task<GetSupplierResponseDto?> GetSupplierAsync(int id);
    Task<IEnumerable<GetSupplierResponseDto>> GetSuppliersAsync();
    Task<GetSupplierResponsePaginatorDto> GetSuppliersPaginatorAsync(int pageNumber, int pageSize, string name);
    Task<IEnumerable<GetSupplierSearchPatronResponseDto>> GetSuppliersByPatronAsync(string patronName);
}

