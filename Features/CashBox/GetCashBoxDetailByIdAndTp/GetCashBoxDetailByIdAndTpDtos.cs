public record GetCashBoxDetalleByIdyTpResponseDto(
    decimal Adelanto,
    decimal Importe,
    string TipoPago,
    int? CustomerId,
    string Cliente,
    string serie,
    string numero,
    string fechaHora
);
public record GetExpeseBoxByIdCashResponseDto(
    DateTime FechaGasto,
    decimal Importe,
    string DetallesEgreso
);



public record GetCashBoxDetailByIdAndTpResponseDto(
    List<GetCashBoxDetalleByIdyTpResponseDto> CashBoxDetail,
    List<GetExpeseBoxByIdCashResponseDto> ExpenseBox    
);
