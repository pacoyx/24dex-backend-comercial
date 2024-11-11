public record RequestCashBoxCreateDto
(    
    decimal SaldoInicial,
    string Observaciones,
    int BranchSalesId,
    int WorkShiftId,
    int UserId
);