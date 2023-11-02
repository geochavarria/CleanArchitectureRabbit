using System.Text.Json.Serialization;

namespace Ecommerce.Services.WebApi.Features
{
    public static class FeatureExtensions
    {
        public static IServiceCollection AddFeatures(this IServiceCollection services, IConfiguration config)
        {
            var myPolice = "policyCustom";

            services.AddCors(options => options.AddPolicy(myPolice,
                b => b.WithOrigins(config["Config:OriginsCors"]!)
                    .AllowAnyHeader()
                    .AllowAnyMethod()));
            services.AddMvc();


            services.AddControllers().AddJsonOptions(opt =>
            {
                var enumConverter = new JsonStringEnumConverter();
                opt.JsonSerializerOptions.Converters.Add(enumConverter);
            });
            return services;
        }
    }
}
