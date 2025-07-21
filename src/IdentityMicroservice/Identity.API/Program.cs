using Autofac;
using Autofac.Extensions.DependencyInjection;
using Identity.Infrastructure;
using JwtTokenAuthentication;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.FileProviders;
using Shared.Application.Interfaces.Logging;
using Shared.BaseApi.Extensions;
using Shared.Infrastructure;
using Shared.Infrastructure.Extensions;
using Shared.Infrastructure.Platform;
using Shared.Infrastructure.Services;
using System.Reflection;

ILoggerService loggerService = new LoggerService();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

    builder.Services.AddHttpContextAccessor();

    var platformService = PlatformServiceFactory.Create();
    builder.Services.AddSingleton(platformService);

    //Authentication and authorization        
    builder.Services.AddJwtTokenAuthentication(platformService);
    //Services
    builder.Services.AddInfrastructureServices();

    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new SharedInfrastructureModule());
        containerBuilder.RegisterModule(new SharedUtilitiesModule());        
    });

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
        InfrastructureServiceRegistration.MigrateDatabase(scope.ServiceProvider);     
    }

    await app.RunAsync();
}
catch (Exception ex)
{
    loggerService.LogError($"{Assembly.GetExecutingAssembly().GetName().Name}-Program - ", ex.Message, ex.InnerException!);
}
