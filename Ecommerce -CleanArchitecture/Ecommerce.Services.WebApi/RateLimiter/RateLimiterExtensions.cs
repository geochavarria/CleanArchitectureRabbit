using Microsoft.AspNetCore.RateLimiting;

namespace Ecommerce.Services.WebApi.RateLimiter
{
    public static class RateLimiterExtensions
    {

        public static IServiceCollection AddRateLimiter(this IServiceCollection services, IConfiguration configuration)
        {
            var fixedWindowPolicy = "fixedWindow";
            services.AddRateLimiter(o =>
            {
                o.AddFixedWindowLimiter(policyName: fixedWindowPolicy, fixedWindows =>
                {
                    fixedWindows.PermitLimit = int.Parse(configuration["RateLimiting:PermitLimit"]);
                    fixedWindows.Window = TimeSpan.FromSeconds(int.Parse(configuration["RateLimiting:Window"]));
                    fixedWindows.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                    fixedWindows.QueueLimit = int.Parse(configuration["RateLimiting:QueueLimit"]);

                });

                o.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });


            return services;


        }

    }
}
