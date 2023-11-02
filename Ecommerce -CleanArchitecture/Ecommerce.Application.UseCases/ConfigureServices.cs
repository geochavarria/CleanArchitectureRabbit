using Ecommerce.Application.Interface.UseCases;
using Ecommerce.Application.UseCases.Categories;
using Ecommerce.Application.UseCases.Customers;
using Ecommerce.Application.UseCases.Discounts;
using Ecommerce.Application.UseCases.Users;
using Ecommerce.Application.Validator;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ecommerce.Application.UseCases
{
    public static class ConfigureServices
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ICustomersApplication, CustomersApplication>();
            services.AddScoped<IUserApplication, UsersApplication>();
            services.AddScoped<ICategoriesApplication, CategoriesApplication>();
            services.AddScoped<IDiscountApplication, DiscountsApplication>();

            services.AddTransient<UsersDtoValidator>();
            services.AddTransient<DiscountDtoValidator>();
            return services;
        }
    }
}
