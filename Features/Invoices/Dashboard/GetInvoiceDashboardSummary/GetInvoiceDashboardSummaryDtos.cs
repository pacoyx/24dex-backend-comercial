public sealed class InvoiceDashboardResponse
{
    public decimal MonthInvoiced { get; set; }
    public decimal MonthCollected { get; set; }
    public decimal AccountsReceivable { get; set; }
    public int InvoiceCount { get; set; }
}
