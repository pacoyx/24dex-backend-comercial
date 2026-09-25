public interface IPurchaseRegisterReportService
{
    Task<List<PurchaseRegisterRowDto>> GetDataAsync(PurchaseRegisterFilter filter);

    Task<byte[]> GeneratePdfAsync(PurchaseRegisterFilter filter);

    Task<byte[]> GenerateExcelAsync(PurchaseRegisterFilter filter);
}
