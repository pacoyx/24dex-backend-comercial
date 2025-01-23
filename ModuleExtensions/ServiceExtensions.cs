

using System.Text;
using _24dex_backend_comercial;
using Dls.Erp.Transversal.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using OpenTelemetry.Logs;

public static class ServiceExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

        // Add services to the container.
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<RecepcionDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularApp",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

        services.AddProblemDetails()
                        .AddExceptionHandler<GlobalExceptionHandler>(); ;


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
            };
        });

        services.AddAuthorization();
        services.AddScoped<JwtService>();
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        // builder.Services.AddWatchDog(builder.Configuration);
        // builder.Services.AddOpenTelemetry().UseAzureMonitor();
        services.AddHealthCheck(configuration);


        // Configurar OpenTelemetry
        // services.AddOpenTelemetry()
        //    .WithTracing(tracerProviderBuilder =>
        //     {
        //         tracerProviderBuilder
        //             .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("DEXTER24webapi"))
        //             .AddAspNetCoreInstrumentation()
        //             .AddHttpClientInstrumentation()
        //             .AddOtlpExporter(options =>
        //             {
        //                 options.Endpoint = new Uri("http://127.0.0.1:4317"); // Cambia esto a la URL de tu OTEL Collector
        //             });
        //     })


        //     .WithMetrics(meterProviderBuilder =>
        //     {
        //         meterProviderBuilder
        //             .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("DEXTER24webapiMetric"))
        //             .AddAspNetCoreInstrumentation()
        //             .AddHttpClientInstrumentation()
        //             .AddRuntimeInstrumentation()
        //             .AddOtlpExporter(options =>
        //             {
        //                 options.Endpoint = new Uri("http://127.0.0.1:4317"); // Cambia esto a la URL de tu OTEL Collector
        //             });
        //     });


        // services.AddLogging(loggingBuilder =>
        // {
        //     loggingBuilder.ClearProviders();
        //     loggingBuilder.AddConsole(); // Para log de consola
        //     loggingBuilder.AddOpenTelemetry(options =>
        //     {
        //         options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("DEXTER24webapi"));
        //         options.AddOtlpExporter(otlpOptions =>
        //         {
        //             otlpOptions.Endpoint = new Uri("http://localhost:4317"); // Dirección del OTEL Collector para logs
        //         });
        //     });
        // });


    }
}
