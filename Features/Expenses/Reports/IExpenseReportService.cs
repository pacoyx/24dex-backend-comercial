public interface IExpenseReportService
{
    Task<List<ExpenseDetailRowDto>> GetByPeriodAsync(ExpenseReportFilter filter);

    Task<List<ExpenseDetailRowDto>> GetWithSupportingDocumentsAsync(ExpenseReportFilter filter);

    Task<List<ExpenseAggregateRowDto>> GetByCategoryAsync(ExpenseReportFilter filter);

    Task<List<ExpenseAggregateRowDto>> GetByBranchAsync(ExpenseReportFilter filter);

    Task<byte[]> GenerateDetailPdfAsync(string title, List<ExpenseDetailRowDto> rows, ExpenseReportFilter filter);

    Task<byte[]> GenerateDetailExcelAsync(List<ExpenseDetailRowDto> rows);

    Task<byte[]> GenerateAggregatePdfAsync(string title, string labelHeader, List<ExpenseAggregateRowDto> rows, ExpenseReportFilter filter);

    Task<byte[]> GenerateAggregateExcelAsync(string labelHeader, List<ExpenseAggregateRowDto> rows);
}
