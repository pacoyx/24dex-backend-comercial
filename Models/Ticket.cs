public class Ticket
{
    public int Id { get; set; }
    public DateTime FechaEmision { get; set; }
    public int ClothingWorkerId { get; set; }
    public  string Status { get; set; } = "";
    public ICollection<TicketClothe> TicketClothes { get; set; } = new List<TicketClothe>();
    public ClothingWorker? ClothingWorker { get; set; }
    public int UserRef { get; set; }
}