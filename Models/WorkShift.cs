public class WorkShift{
    public int Id { get; set; }
    public string Descripcion { get; set; } = "";
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraCierre { get; set; }
    public string Observaciones { get; set; } = "";    
    public string EstadoRegistro { get; set; } = ""; // A: Activo, I: Inactivo    
}