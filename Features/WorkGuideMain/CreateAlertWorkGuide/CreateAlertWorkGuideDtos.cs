public record RequestCreateAlertMsgDto
{
    public string Titulo { get; set; } = string.Empty;
    public string TipoAlerta { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;
    public int WorkGuideMainId { get; set; }
}
