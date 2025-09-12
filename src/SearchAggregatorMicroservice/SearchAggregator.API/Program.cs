using Autofac;
using Autofac.Extensions.DependencyInjection;
using JwtTokenAuthentication;
using SearchAggregator.Application.Clients;
using SearchAggregator.Infrastructure;
using SearchAggregator.Infrastructure.Clients;
using Shared.Application.Interfaces.Logging;
using Shared.BaseApi.Extensions;
using Shared.Infrastructure;
using Shared.Infrastructure.Extensions;
using System.Text.Json.Serialization;

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

builder.Services.AddHttpClient<IProductClient, ProductClient>(client =>
{
    var baseUrl = builder.Configuration.GetValue<string>("Microservice-Endpoint:Product-BaseUrl");
    client.BaseAddress = new Uri($"{baseUrl}/api/v1/");
});

builder.Services.AddHttpClient<IAstrologerClient, AstrologerClient>(client =>
{
    var baseUrl = builder.Configuration.GetValue<string>("Microservice-Endpoint:Astrologer-BaseUrl");
    client.BaseAddress = new Uri($"{baseUrl}/api/v1/");
});

builder.Services.AddHttpClient<IPriestClient, PriestClient>(client =>
{
    var baseUrl = builder.Configuration.GetValue<string>("Microservice-Endpoint:Priest-BaseUrl");
    client.BaseAddress = new Uri($"{baseUrl}/api/v1/");
});

builder.Services.AddHttpClient<ITempleClient, TempleClient>(client =>
{
    var baseUrl = builder.Configuration.GetValue<string>("Microservice-Endpoint:Temple-BaseUrl");
    client.BaseAddress = new Uri($"{baseUrl}/api/v1/");
});

builder.Services.AddHttpClient<IKathavachakClient, KathavachakClient>(client =>
{
    var baseUrl = builder.Configuration.GetValue<string>("Microservice-Endpoint:Kathavachak-BaseUrl");
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

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseExceptionHandler(options => { });

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    loggerService.LogError(ex, "An error occurred in SEARCH-AGGREGATOR-API-Program.");
    throw;
}