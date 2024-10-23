
public record ResponseCashBoxMainAllDTO
(    
     int Id, 
     DateTime FechaCaja, 
     DateTime FechaHoraApertura, 
     DateTime? FechaHoraCierre, 
     string EstadoCaja, 
     decimal SaldoInicial, 
     decimal SaldoFinal, 
     decimal TotalIngreso, 
     decimal TotalSalida, 
     string Observaciones, 
     string ObservacionesCierre, 
     string EstadoRegistro, 
     int BranchSalesId, 
     int WorkShiftId, 
     int UserId, 
     IEnumerable<ResponseCashBoxDetailAllDTO>? CashBoxDetails
);


public record ResponseCashBoxDetailAllDTO
(
    int Id,
    string TipoComprobante,
    string SerieComprobante,
    string NumComprobante,
    DateTime FechaComprobante,
    decimal Importe,
    decimal Adelanto,
    string TipoPago,
    string DescripcionPago,
    string Observaciones,
    string EstadoRegistro,
    Customer Customer,
    int CashBoxMainId    
);
