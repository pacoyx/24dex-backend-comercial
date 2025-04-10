using HealthChecks.UI.Client;
using FluentValidation.AspNetCore;
using FluentValidation;
using Serilog;



var currentAssembly = typeof(Program).Assembly;
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(options => options.AddServerHeader = false);
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.AddServicesDi();

// builder.Services.AddApplicationInsightsTelemetry();

// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Services.AddSerilog();


builder.Services
    // Plumbing/Dependencies
    .AddAutoMapper(currentAssembly)
    .AddMediatR(o => o.RegisterServicesFromAssembly(currentAssembly))
    .AddFluentValidationAutoValidation()
    .AddValidatorsFromAssembly(currentAssembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middlewares de seguridad y autenticación
app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthentication();
app.UseAuthorization();

// OutputCache debe estar ANTES de los endpoints
app.UseOutputCache();

// Health Checks
app.MapHealthChecksUI();
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// secutity headers
// app.RemoveInsecureHeaders();
// app.UseEmojiMiddleware();

// errores
app.UseStatusCodePages();
app.UseExceptionHandler();



// Rutas (endpoints)
app.MapRoutes();



app.Run();