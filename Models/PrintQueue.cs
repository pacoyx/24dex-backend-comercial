namespace DexterCompany.Models;

public class PrintQueue
{
    public long Id { get; set; }

    public int WorkGuideId { get; set; }

    public int PrinterId { get; set; }

    public PrintStatus Status { get; set; }

    public int RetryCount { get; set; }

    public string DocumentType { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? ProcessedAt { get; set; }

    public string? ErrorMessage { get; set; }

    public WorkGuideMain WorkGuideMain { get; set; } = null!;

    public Printer Printer { get; set; } = null!;
    public int PrintCopy { get; set; }
}