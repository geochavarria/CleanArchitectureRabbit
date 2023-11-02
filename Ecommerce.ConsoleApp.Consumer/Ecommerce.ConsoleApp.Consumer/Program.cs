// See https://aka.ms/new-console-template for more information
using Ecommerce.ConsoleApp.Consumer;
using MassTransit;
using Microsoft.Extensions.Hosting;
public class Program
{
    public async static Task Main(string[] args)
    {
        await  Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                services.AddMassTransit(x=>
                {
                    x.AddConsumer<DiscountCreatedConsumer>();
                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host("localhost", "/", h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });

                        cfg.ConfigureEndpoints(context);
                    });
                });  
            })
            .Build()
            .RunAsync();
    }
}


