using Identity.Infrastructure;
using JwtTokenAuthentication;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.FileProviders;
using Shared.Application.Common.Services.Interfaces;
using Shared.BaseApi.Extensions;
using Shared.Infrastructure.Extensions;
using Shared.Infrastructure.Services;
using System.Reflection;

ILoggerService loggerService = new LoggerService();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddHttpContextAccessor();
    //Authentication and authorization        
    builder.Services.AddJwtTokenAuthentication();
    //Services
    builder.Services.AddInfrastructureServices();

    // Custom exception handler
    builder.Services.AddExceptionHandler<CustomExceptionHandler>();

    //Cors
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("EnableCORS", builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
    });

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGenerate();

    builder.Services.AddDirectoryBrowser();

    var app = builder.Build();

    app.MapHealthChecks("/healthz", new HealthCheckOptions
    {
        ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    },
        AllowCachingResponses = true,
    }).RequireHost("*:7190").RequireCors("EnableCORS");

    // Configure the HTTP request pipeline.

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();
    app.UseFileServer(new FileServerOptions
    {
        FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "StaticFiles")),
        EnableDirectoryBrowsing = true,
    });

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseExceptionHandler(options => { });

    app.MapControllers();

    using (var scope = app.Services.CreateScope())
    {
        JwtTokenAuthenticationRegistration.MigrateConnectionString(scope.ServiceProvider);
        InfrastructureServiceRegistration.MigrateDatabase(scope.ServiceProvider);
        JwtTokenAuthenticationRegistration.MigrateDatabase(scope.ServiceProvider);        
    }

    await app.RunAsync();
}
catch (Exception ex)
{
    loggerService.LogError($"{Assembly.GetExecutingAssembly().GetName().Name}-Program - ", ex.Message, ex.InnerException!);
}
