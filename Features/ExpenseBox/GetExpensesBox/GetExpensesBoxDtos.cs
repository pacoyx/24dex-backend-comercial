public record GetExpensesBoxDtoResponse(
 int Id,
 string CategoryGasto,
 string PersonalAutoriza,
 DateTime FechaGasto,
 decimal Importe,
 string DetallesEgreso,
 string EstadoRegistro
    );