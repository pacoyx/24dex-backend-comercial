
public class WorkerNode
{
    public int Id { get; set; }

    public int BranchId { get; set; }

    public string WorkerKey { get; set; } = string.Empty;

    public string ComputerName { get; set; } = string.Empty;

    public DateTime? LastConnectionAt { get; set; }

    public BranchSales Branch { get; set; } = null!;
    
    public bool IsActive { get; set; } = true;

    public ICollection<Printer> Printers { get; set; }
        = new List<Printer>();
}