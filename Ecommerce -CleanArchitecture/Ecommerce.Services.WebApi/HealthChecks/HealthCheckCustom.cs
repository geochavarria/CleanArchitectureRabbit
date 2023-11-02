using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Ecommerce.Services.WebApi.HealthChecks
{
    public class HealthCheckCustom : IHealthCheck
    {
        private readonly Random _random = new Random();
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var responseTime = _random.Next();
            if(responseTime <100) { 
                return Task.FromResult(HealthCheckResult.Healthy("Healty Result from HealthCheckCustom"));
            }else if (responseTime < 200)
            {
                
            }

            return Task.FromResult(HealthCheckResult.Healthy("Unhealthy Result from HealthCheckCustom"));
        }
    }
}
