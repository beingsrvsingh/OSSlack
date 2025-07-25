using SecretManagement.Infrastructure;
using JwtTokenAuthentication;
using Shared.BaseApi.Extensions;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Shared.Infrastructure;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities;


var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddJwtTokenAuthentication();
builder.Services.AddInfrastructureServices();

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new SecretManagementInfrastructureModule());
    containerBuilder.RegisterModule(new SharedInfrastructureModule());
    containerBuilder.RegisterModule(new SharedUtilitiesModule());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenerate();

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
    app.UseAuthorization();

    app.MapControllers();

    using (var scope = app.Services.CreateScope())
    {
        InfrastructureServiceRegistration.MigrateDatabase(scope.ServiceProvider);
    }

    app.Run();
}
catch (Exception ex)
{
    loggerService.LogError(ex, "An error occurred in SecretManagement-API-Program.");
    throw;
}

