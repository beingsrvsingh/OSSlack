using SecretManagement.Infrastructure;
using JwtTokenAuthentication;
using Shared.Application.Common.Services.Interfaces;
using Shared.BaseApi.Extensions;
using Shared.Infrastructure.Services;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Shared.Infrastructure;

ILoggerService loggerService = new LoggerService();
try
{
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
    Console.WriteLine($"An error occurred while starting the application: {ex.InnerException}");
    loggerService.LogError("Secret-Management-API-Program - ", ex.Message, ex.InnerException!);
}

