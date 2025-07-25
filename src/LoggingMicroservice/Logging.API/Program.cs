using JwtTokenAuthentication;
using Logging.Infrastructure;
using Shared.Application.Interfaces.Logging;
using Shared.BaseApi.Extensions;
using Shared.Infrastructure.Platform;

var builder = WebApplication.CreateBuilder(args);

//Authentication and authorization        
builder.Services.AddJwtTokenAuthentication();
builder.Services.AddInfrastructureServices();

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
    loggerService.LogError(ex, "An error occurred in Identity-API-Program.");

    throw;
}
