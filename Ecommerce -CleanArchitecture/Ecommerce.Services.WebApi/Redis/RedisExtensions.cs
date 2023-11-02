namespace Ecommerce.Services.WebApi.Redis
{
    public static class RedisExtensions
    {

        public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration config)
        {

            services.AddStackExchangeRedisCache(O =>
            {
                O.Configuration = config.GetConnectionString("RedisConnection");
            });


            return services;
        }
    }
}
