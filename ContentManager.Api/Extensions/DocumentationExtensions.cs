using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ContentManager.Api.Extensions
{
    public static class DocumentationExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IConfiguration configuration)
        {
            var apiInfo = configuration.GetSection("ApplicationInfo").Get<OpenApiInfo>();
            return services.AddSwaggerGen(config =>
            {
                config.ConfigureApiDocumentationDetails(apiInfo!).ConfigureJwtSecutiryScheme();
            });
        }

        private static SwaggerGenOptions ConfigureApiDocumentationDetails(this SwaggerGenOptions config, OpenApiInfo info)
        {
            config.SwaggerDoc(info.Version, info);
            return config;
        }

        private static void ConfigureJwtSecutiryScheme(this SwaggerGenOptions config)
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put only your JWT Bearer token on the textbox below.",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            config.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
            config.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme, Array.Empty<string>() }
            });
        }
    }
}
