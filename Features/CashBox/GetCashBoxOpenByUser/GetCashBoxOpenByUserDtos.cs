public record ResponseCashBoxOpenDto
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
    int UserId
);