public class ExpenseAttachment
{
    public long Id { get; set; }

    public long ExpenseId { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string OriginalFileName { get; set; } = string.Empty;

    public string BlobUrl { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public Expense Expense { get; set; } = null!;
}
