public record RequestCashBoxCreateDto
(
    DateTime FechaCaja,
    DateTime FechaHoraApertura,
    decimal SaldoInicial,
    string Observaciones,
    int BranchSalesId,
    int WorkShiftId,
    int UserId
);