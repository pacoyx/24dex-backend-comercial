public record ResponseAlertsByWorkGuideDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string TipoAlerta { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;
}