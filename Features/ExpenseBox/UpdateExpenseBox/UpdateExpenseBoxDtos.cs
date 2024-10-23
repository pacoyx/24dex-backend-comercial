public record UpdateExpenseBoxDto(
    string CategoryGasto,
    string PersonalAutoriza,
    DateTime FechaGasto,
    decimal Importe,
    string DetallesEgreso,
    string EstadoRegistro,
    int UserId,
    int CashBoxMainId
);