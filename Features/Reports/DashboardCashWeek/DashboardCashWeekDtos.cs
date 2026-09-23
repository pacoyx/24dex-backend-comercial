public record DashboardCashWeekResponseDto(
    DateTime Fecha,
    string DiaSemana,
    decimal MontoTotal,
    List<DashboardCashDetailDto> Detalles
);
