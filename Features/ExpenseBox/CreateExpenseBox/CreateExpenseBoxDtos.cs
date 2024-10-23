public record CreateExpenseBoxDto(
    string CategoryGasto,
    string PersonalAutoriza,
    DateTime FechaGasto,
    decimal Importe,
    string DetallesEgreso,
    string EstadoRegistro
);

public record CreateExpenseBoxDetailsDto(
    int Id,
    string CategoryGasto,
    string PersonalAutoriza,
    DateTime FechaGasto,
    decimal Importe,
    string DetallesEgreso    
);
