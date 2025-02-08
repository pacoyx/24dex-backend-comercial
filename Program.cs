using HealthChecks.UI.Client;
using FluentValidation.AspNetCore;
using FluentValidation;

var currentAssembly = typeof(Program).Assembly;
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(options => options.AddServerHeader = false);
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.AddServicesDi();


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