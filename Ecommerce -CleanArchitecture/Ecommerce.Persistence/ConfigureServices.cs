using Ecommerce.Application.Interface.Persistence;
using Ecommerce.Persistence.Context;
using Ecommerce.Persistence.Interceptors;
using Ecommerce.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Persistence
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<DapperContext>();
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.EnableDetailedErrors(true);
                opt.UseSqlServer(config.GetConnectionString("NorthwindConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });
            services.AddScoped<ICustomersRepository, CustomerRepository>();
            services.AddScoped<IUserRepository, UsersRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
