public record CreateExpenseBoxDto(
    string CategoriaGasto,
    string PersonalAutoriza,    
    decimal Importe,
    string DetallesEgreso,
    string EstadoRegistro,
     int UserId     
);

public record CreateExpenseBoxDetailsDto(
    int Id,
    string CategoryGasto,
    string PersonalAutoriza,
    DateTime FechaGasto,
    decimal Importe,
    string DetallesEgreso
);
