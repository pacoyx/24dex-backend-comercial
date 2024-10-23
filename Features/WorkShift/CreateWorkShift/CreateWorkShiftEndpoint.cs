public static class CreateWorkShiftEndpoint{
    public static void MapCreateWorkShift(this IEndpointRouteBuilder app){
        app.MapPost("/", async (RecepcionDbContext db, WorkShiftCreateDto workShiftCreateDto) =>{

            // validaciones de hora inicio y cierre
            if (workShiftCreateDto.HoraInicio == null || workShiftCreateDto.HoraCierre == null)
            {
                return Results.BadRequest("La hora de inicio y cierre son obligatorias");
            }
            
            var horaInicio = int.Parse(workShiftCreateDto.HoraInicio.Split(":")[0]);
            var minutosInicio = int.Parse(workShiftCreateDto.HoraInicio.Split(":")[1]);

            var horaCierre = int.Parse(workShiftCreateDto.HoraCierre.Split(":")[0]);
            var minutosCierre = int.Parse(workShiftCreateDto.HoraCierre.Split(":")[1]);

            var workShift = new WorkShift
            {
                Descripcion = workShiftCreateDto.Descripcion,
                HoraInicio = new TimeSpan(horaInicio, minutosInicio, 0),
                HoraCierre = new TimeSpan(horaCierre, minutosCierre, 0),
                Observaciones = workShiftCreateDto.Observaciones,
                EstadoRegistro = "A"
            };
            await db.WorkShifts.AddAsync(workShift);
            await db.SaveChangesAsync();
            return Results.Ok(workShift);
        });
    }
}