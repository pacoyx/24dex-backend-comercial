public class TicketClothe
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public int Item { get; set; }
    public int ClothingItemId { get; set; }
    public string CustomObservations { get; set; } = string.Empty;
    public string LaundryObservations { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public ICollection<ClothingObservations>  clothingObservations { get; set; } = new List<ClothingObservations>();
}