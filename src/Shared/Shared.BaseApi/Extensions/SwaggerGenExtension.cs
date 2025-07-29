using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Shared.BaseApi.Extensions;

public static class SwaggerExtension
{
    public static IServiceCollection AddSwaggerGenerate(this IServiceCollection services)
    {
        services.AddApiVersioningExtension();

        services.AddSwaggerGen(opt =>
        {
            var provider = services.BuildServiceProvider()
                     .GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var item in provider.ApiVersionDescriptions)
            {
                opt.SwaggerDoc(item.GroupName, new OpenApiInfo
                {
                    Title = $"{Assembly.GetEntryAssembly()?.GetName().Name}  {item.GroupName}",
                    Version = item.ApiVersion.MajorVersion.ToString() + "." + item.ApiVersion.MinorVersion
                });
            }

            // To Hide the api endpoints
            //opt.DocInclusionPredicate((version, desc) =>
            //{
            //    if (!desc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;
            //    var versions = methodInfo.DeclaringType?.GetCustomAttributes(true).OfType<ApiVersionAttribute>().SelectMany(attr => attr.Versions);
            //    var maps = methodInfo.GetCustomAttributes(true).OfType<MapToApiVersionAttribute>().SelectMany(attr => attr.Versions).ToArray();
            //    version = version.Replace("v", "");
            //    return versions!.Any(v => v.ToString() == version && maps.Any(v => v.ToString() == version));
            //});

            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[]{}
            }
            });
        });
        return services;
    }
}
