public record CashBoxDetailCreateDTO{
    public string TipoComprobante { get; set; } = "";
    public string SerieComprobante { get; set; } = "";
    public string NumComprobante { get; set; } = "";
    public DateTime FechaComprobante { get; set; }
    public decimal Importe { get; set; }
    public decimal Adelanto { get; set; }
    public string TipoPago { get; set; } = ""; // E: Efectivo, T: Tarjeta, D: Deposito, QR: Codigo QR, SP: Sin Pago
    public string DescripcionPago { get; set; } = ""; // Efectivo, Tarjeta, Deposito, Codigo QR, Sin Pago
    public string Observaciones { get; set; } = "";
    public string EstadoRegistro { get; set; } = ""; // A: Activo, I: Inactivo
    public int CustomerId { get; set; }
    public int CashBoxMainId { get; set; }
}