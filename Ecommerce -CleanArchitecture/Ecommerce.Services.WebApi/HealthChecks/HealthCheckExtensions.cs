namespace Ecommerce.Services.WebApi.HealthChecks
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var strConnection = configuration.GetConnectionString("NorthwindConnection");
            services.AddHealthChecks()
                .AddSqlServer(strConnection, tags: new[] { "database" })
                .AddRedis(configuration.GetConnectionString("RedisConnection"), tags: new[] { "cache" })
                .AddCheck<HealthCheckCustom>("HealthCheckCustom", tags: new[] { "Custom" });
            services.AddHealthChecksUI().AddInMemoryStorage();

            return services;
        }

    }
}
