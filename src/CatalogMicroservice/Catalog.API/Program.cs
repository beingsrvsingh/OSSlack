using Autofac;
using Autofac.Extensions.DependencyInjection;
using Catalog.Infrastructure;
using Catalog.Infrastructure.Repositories;
using JwtTokenAuthentication;
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
    });

    // Custom exception handler
    builder.Services.AddExceptionHandler<CustomExceptionHandler>();

    builder.Services.AddScoped<ISampleMongoRepository, SampleMongoRepository>();

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

    var app = builder.Build();

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

    loggerService.LogError($"{Assembly.GetExecutingAssembly().GetName().Name}-Program - ", ex.Message, ex.InnerException!);
}