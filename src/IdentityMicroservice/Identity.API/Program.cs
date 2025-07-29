using Autofac;
using Autofac.Extensions.DependencyInjection;
using Identity.Infrastructure;
using JwtTokenAuthentication;
using Microsoft.Extensions.FileProviders;
using Shared.Application.Interfaces.Logging;
using Shared.BaseApi.Extensions;
using Shared.Infrastructure;
using Shared.Infrastructure.Extensions;
using Shared.Utilities;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddHttpContextAccessor();

//Authentication and authorization        
builder.Services.AddJwtTokenAuthentication();
//Services
builder.Services.AddInfrastructureServices();

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new IdentityInfrastructureModule());
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

// Register essential framework services like IHttpClientFactory and IMemoryCache here in Program.cs.
// These services are core infrastructure components provided by Microsoft.Extensions.DependencyInjection.
// Registering them at this level ensures they are available throughout the microservice and properly integrated with Autofac.
// Avoid registering these inside Autofac modules, as these methods are specific to IServiceCollection and must be configured early.

builder.Services.AddHttpClient("SecretManagerClient", client =>
{
    var config = Configuration.LoadAppSettings();
    var baseUrl = config.GetSection("Secrets").GetValue<String>("BaseUrl") ?? throw new InvalidOperationException("Secrets:BaseUrl is not configured.");
    client.BaseAddress = new Uri($"{baseUrl}/api/v1/");
});
builder.Services.AddMemoryCache();  

var app = builder.Build();

var loggerService = app.Services.GetRequiredService<ILoggerService<Program>>();
try
{
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
    loggerService.LogError(ex, "An error occurred in Identity-API-Program.");
    throw;
}
