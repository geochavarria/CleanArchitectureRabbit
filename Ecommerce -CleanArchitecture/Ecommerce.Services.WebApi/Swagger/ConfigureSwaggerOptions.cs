using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ecommerce.Services.WebApi.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider; 
        
        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Version = description.ApiVersion.ToString(),
                Title = "Ecommerce Technology Services Api Market",
                Description = "A Simple Example ASP.NET Core Web API",
                TermsOfService = new Uri("https://example.com"),
                Contact = new OpenApiContact
                {
                    Name = "Edwin Chavarria",
                    Email = "echavarria@ugb.edu.sv",
                    Url = new Uri("https://example.com")
                },
                License = new OpenApiLicense
                {
                    Name = "Use under Licx",
                    Url = new Uri("https://example.com")
                }
            };


            if (description.IsDeprecated)
            {
                info.Description += "Esta version de la api esta obsoleta";
            }

            return info; 
        }
    
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions) {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            
            }
        }
    }
}
