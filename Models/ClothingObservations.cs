public class ClothingObservations{
    public int Id { get; set; }
    public int TicketClotheId { get; set; }
    public int TypeObservationId { get; set; }
    public int ObservationSectionId { get; set; }
    public string Observations { get; set; } = string.Empty;    
    public string Status { get; set; } = string.Empty;
}