using Autofac;
using Autofac.Extensions.DependencyInjection;
using JwtTokenAuthentication;
using Review.Infrastructure;
using Shared.Application.Common.Services.Interfaces;
using Shared.BaseApi.Extensions;
using Shared.Infrastructure;
using Shared.Infrastructure.Extensions;
using Shared.Infrastructure.Services;

ILoggerService loggerService = new LoggerService();

try
{
    var builder = WebApplication.CreateBuilder(args);    

    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

    //JWT Token authentication service
    builder.Services.AddJwtTokenAuthentication();

    // Add services to the container.    
    builder.Services.AddInfrastructureServices();

    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new SharedInfrastructureModule());
        containerBuilder.RegisterModule(new SharedUtilitiesModule());        
    });

    // Custom exception handler
    builder.Services.AddExceptionHandler<CustomExceptionHandler>();

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddControllers();
    builder.Services.AddMvc().ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddApiVersioningExtension();
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

    app.UseCors("EnableCORS");

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.UseExceptionHandler(options => { });

    using (var scope = app.Services.CreateScope())
    {
        InfrastructureServiceRegistration.MigrateDatabase(scope.ServiceProvider);
    }

    app.Run();

}
catch (Exception ex)
{
    loggerService.LogError("Review-API-Program - ", ex.Message, ex.InnerException!);
}