using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Order.Infrastructure;
using JwtTokenAuthentication;
using Shared.Application.Interfaces.Logging;
using Shared.BaseApi.Extensions;
using Shared.Infrastructure;
using Shared.Infrastructure.Extensions;
using Order.Application.Services;
using Order.Infrastructure.Services;

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

builder.Services.AddHttpClient<IPaymentService, PaymentService>(client =>
{
    var baseUrl = builder.Configuration.GetValue<string>("Microservice-Endpoint:Payment-BaseUrl");
    client.BaseAddress = new Uri($"{baseUrl}/api/v1/payment/");
});

builder.Services.AddHttpClient<IAddressService, AddressService>(client =>
{
    var baseUrl = builder.Configuration.GetValue<string>("Microservice-Endpoint:Address-BaseUrl");
    client.BaseAddress = new Uri($"{baseUrl}/api/v1/address/");
});

builder.Services.AddHttpClient<IProductService, ProductService>(client =>
{
    var baseUrl = builder.Configuration.GetValue<string>("Microservice-Endpoint:Product-BaseUrl");
    client.BaseAddress = new Uri($"{baseUrl}/api/v1/product/");
});


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenerate();

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
    loggerService.LogError(ex, "An error occurred in Order-API-Program.");
    throw;
}