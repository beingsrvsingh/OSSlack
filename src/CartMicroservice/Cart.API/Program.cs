using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Cart.Application.Services;
using Cart.Infrastructure.Services;
using CartMicroservice.Infrastructure;
using JwtTokenAuthentication;
using Shared.Application.Interfaces.Logging;
using Shared.BaseApi.Extensions;
using Shared.Infrastructure;
using Shared.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddHttpContextAccessor();

//Authentication and authorization        
builder.Services.AddJwtTokenAuthentication();
//Services
builder.Services.AddInfrastructureServices();

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new InfrastructureModule());
    containerBuilder.RegisterModule(new SharedInfrastructureModule());
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

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenerate();

builder.Services.AddHttpClient("Product", client =>
{
    var baseUrl = builder.Configuration["Microservice-Endpoint:Product-BaseUrl"];
    client.BaseAddress = new Uri($"{baseUrl}/api/v1/");
});

builder.Services.AddHttpClient("Astrologer", client =>
{
    var baseUrl = builder.Configuration["Microservice-Endpoint:Astrologer-BaseUrl"];
    client.BaseAddress = new Uri($"{baseUrl}/api/v1/");
});

builder.Services.AddHttpClient("Priest", client =>
{
    var baseUrl = builder.Configuration["Microservice-Endpoint:Priest-BaseUrl"];
    client.BaseAddress = new Uri($"{baseUrl}/api/v1/");
});

builder.Services.AddHttpClient("Temple", client =>
{
    var baseUrl = builder.Configuration["Microservice-Endpoint:Temple-BaseUrl"];
    client.BaseAddress = new Uri($"{baseUrl}/api/v1/");
});


var app = builder.Build();

var loggerService = app.Services.GetRequiredService<ILoggerService<Program>>();

try
{

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("EnableCORS");

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseExceptionHandler(options => { });

    app.MapControllers();

    using (var scope = app.Services.CreateScope())
    {
        InfrastructureServiceRegistration.MigrateDatabase(scope.ServiceProvider);
    }

    app.Run();

}
catch (Exception ex)
{
    loggerService.LogError(ex, "An error occurred in Cart-API-Program.");
    throw;
}