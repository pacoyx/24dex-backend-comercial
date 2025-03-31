using System.ComponentModel.DataAnnotations;

public class NumbersDocument
{
    public int Id { get; set; }
    public int BranchId { get; set; }
    [StringLength(2)]
    public string TypeDoc { get; set; } = string.Empty;
    [StringLength(5)]
    public string SerieDoc { get; set; } = string.Empty;
    [StringLength(10)]
    public int NumberDoc { get; set; }
    [StringLength(1)]
    public string Status { get; set; } = string.Empty;
    [StringLength(5)]
    public string TypeProcess { get; set; } = string.Empty;

}