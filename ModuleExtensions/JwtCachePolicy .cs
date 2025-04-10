using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.OutputCaching;

public class JwtCachePolicy : IOutputCachePolicy
{

    public ValueTask CacheRequestAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        
        // Usar el JWT como parte de la clave de caché
        if (!string.IsNullOrEmpty(authHeader))
        {


             var jwtHash = ComputeStableHash(authHeader);

            Console.WriteLine($" ENTREO ======== JWT recibido: {"user_" + jwtHash}");
            context.Tags.Add($"user_{jwtHash}");
            context.CacheVaryByRules.HeaderNames = 
                new Microsoft.Extensions.Primitives.StringValues(context.CacheVaryByRules.HeaderNames.Append("Authorization").ToArray());
        }

        return ValueTask.CompletedTask;
    }

      private static string ComputeStableHash(string input)
    {
        using var sha256 = SHA256.Create();
        byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(hashBytes);
    }

    public ValueTask ServeFromCacheAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }

    public ValueTask ServeResponseAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }

    // Resto de implementación requerida por la interfaz...
}