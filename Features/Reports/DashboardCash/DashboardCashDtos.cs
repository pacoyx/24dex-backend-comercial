public record DashboardCashResponseDto(
    int BranchSalesId,
    string Descripcion,
    List<DashboardCashDetailDto> Detalles
);

public record DashboardCashDetailDto(
    string TipoPago,
    decimal MontoTotal
);