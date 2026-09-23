public interface ISalesRegisterReportService
{
    Task<List<SalesRegisterRowDto>> GetDataAsync(SalesRegisterFilter filter);

    Task<byte[]> GeneratePdfAsync(SalesRegisterFilter filter);

    Task<byte[]> GenerateExcelAsync(SalesRegisterFilter filter);
}
