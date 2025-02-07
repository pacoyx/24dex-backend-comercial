using HealthChecks.UI.Client;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(options => options.AddServerHeader = false);
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.AddServicesDi();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//middlewares
app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthentication();
app.UseAuthorization();
app.MapHealthChecksUI();
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// secutity headers
app.RemoveInsecureHeaders();

// errores
app.UseStatusCodePages();
app.UseExceptionHandler();

app.UseEmojiMiddleware();

// routes
app.MapRoutes();

app.Run();