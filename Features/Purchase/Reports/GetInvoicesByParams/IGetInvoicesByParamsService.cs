public interface IGetInvoicesByParamsService{
    Task<InvoiceListPaginatorResponseDto> GetInvoicesByMonth(int pageNumber, int pageSize, int month, int year);
    Task<InvoiceListPaginatorResponseDto> GetInvoicesByDate(int pageNumber, int pageSize, DateTime startDate, DateTime endDate);    
    Task<InvoiceListPaginatorResponseDto> GetInvoicesBySupplier(int pageNumber, int pageSize, int supplierId);    
    Task<InvoiceListPaginatorResponseDto> GetInvoicesByProduct(int pageNumber, int pageSize, string productNamePatron);
}