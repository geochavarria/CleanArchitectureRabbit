using commerce.Application.Interface.Infraestructure;
using Ecommerce.Infraestructure.EventBus;
using Ecommerce.Infraestructure.EventBus.Options;
using Ecommerce.Infraestructure.Notifications;
using Ecommerce.Infraestructure.Notifications.Options;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SendGrid.Extensions.DependencyInjection;

namespace Ecommerce.Infraestructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services)
        {

            services.ConfigureOptions<RabbitMqOptionsSetup>();
            services.AddScoped<IEventBus, EventBusRabbitMQ>();
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    RabbitMqOptions opt = services.BuildServiceProvider()
                    .GetRequiredService<IOptions<RabbitMqOptions>>()
                    .Value;

                    cfg.Host(opt.HostName, opt.VirtualHost, h =>
                    {
                        h.Username(opt.UserName);
                        h.Password(opt.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });

            });


            //Sendgrid
            services.AddScoped<INotification, NotificationSendgrid>();
            services.ConfigureOptions<SendgridOptionsSetup>();
            SendgridOptions? sendgridOptions = services.BuildServiceProvider()
                .GetRequiredService<IOptions<SendgridOptions>>()
                .Value;

            services.AddSendGrid(opt =>
            {
                opt.ApiKey = sendgridOptions.ApiKey;
            });



            return services;
        }
    }
}
