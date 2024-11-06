public record RequestDevolucionDataDto{
    public bool DevolverEfectivo { get; set; }
    public decimal Monto { get; set; }
    public int UserId { get; set; }
}