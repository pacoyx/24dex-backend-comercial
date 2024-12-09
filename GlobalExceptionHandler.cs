using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;

namespace _24dex_backend_comercial;

public class GlobalExceptionHandler : IExceptionHandler
{

    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
        )
    {
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        this._logger.LogError(exception,
            "No se pudo procesar un request en la pc {MachineName}. TraceId: {traceId}",
            Environment.MachineName,
            traceId);

        var (statusCode, title) = MapException(exception);

        await Results.Problem(
           title: title,
            statusCode: statusCode,
            extensions: new Dictionary<string, object?>
            {
                {"traceId", traceId}
            })
            .ExecuteAsync(httpContext);

        return true;
    }

    private static (int StatusCode, string Title) MapException(Exception exception)
    {
        return exception switch
        {
            ArgumentOutOfRangeException _ => (StatusCodes.Status400BadRequest, "Argumento fuera de rango"),
            ArgumentException _ => (StatusCodes.Status400BadRequest, "Argumento inválido"),
            InvalidOperationException _ => (StatusCodes.Status400BadRequest, "Operación inválida"),
            KeyNotFoundException _ => (StatusCodes.Status404NotFound, "Elemento no encontrado"),
            _ => (StatusCodes.Status500InternalServerError, "Tenemos un problema pero lo estamos resolviendolo")
        };

    }
}
