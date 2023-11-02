using Ecommerce.Transversal.Common;
using Ecommerce.Transversal.Loggin;

namespace Ecommerce.Services.WebApi.Injection
{
    public static class InjectionExtensions
    {

        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            //services.AddSingleton<IConnectionFactory, ConnectionFactory>();

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            return services;
        }
    }
}
