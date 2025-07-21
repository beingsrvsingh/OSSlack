using JwtTokenAuthentication;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;
using Shared.Infrastructure.Platform;

var builder = WebApplication.CreateBuilder(args);

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configFolder = Path.Combine(Directory.GetCurrentDirectory(), "Configs");

foreach (var file in Directory.GetFiles(configFolder, $"*.ocelot.{env}.json"))
{
    builder.Configuration.AddJsonFile(file, optional: true, reloadOnChange: true);
}

builder.Services.AddOcelot(builder.Configuration).AddCacheManager(settings => settings.WithDictionaryHandle()).AddPolly();

    var platformService = PlatformServiceFactory.Create();
    builder.Services.AddSingleton(platformService);

    //Authentication and authorization        
    builder.Services.AddJwtTokenAuthentication(platformService);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

await app.UseOcelot();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
