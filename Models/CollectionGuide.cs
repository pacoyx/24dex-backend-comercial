public class CollectionGuide
{
    public int Id { get; set; }
    public DateTime FechaEmision { get; set; }
    public string LaundryComplianceStatus { get; set; } = "";
    public string CustomerComplianceStatus { get; set; } = "";
    public string Carrier { get; set; } = string.Empty;
    public string Observations { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public ICollection<CollectionGuideTicket> CollectionGuideTickets { get; set; } = new List<CollectionGuideTicket>();
    public int UserRef { get; set; }
}