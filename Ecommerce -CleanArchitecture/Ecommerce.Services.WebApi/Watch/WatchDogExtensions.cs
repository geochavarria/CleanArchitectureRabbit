using WatchDog;

namespace Ecommerce.Services.WebApi.Watch
{
    public static class WatchDogExtensions
    {
        public static IServiceCollection AddWatchDog(this IServiceCollection services, IConfiguration config)
        {

            services.AddWatchDogServices(opt =>
            {
                opt.SetExternalDbConnString = config.GetConnectionString("NorthwindConnection");
                opt.DbDriverOption = WatchDog.src.Enums.WatchDogDbDriverEnum.MSSQL;
                opt.IsAutoClear = true;
                opt.ClearTimeSchedule = WatchDog.src.Enums.WatchDogAutoClearScheduleEnum.Monthly;
            });
            return services;
        }
    }
}
