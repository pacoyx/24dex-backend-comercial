public record RequestRecogerItemDto
{
    public bool CobrarEfectivo { get; set; }
    public decimal Monto { get; set; }
    public int UserId { get; set; }
    public string TipoPago { get; set; } = "";
    public string DescripcionPago { get; set; } = "";
}