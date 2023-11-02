using Ecommerce.Domain.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.ConsoleApp.Consumer
{
    internal class DiscountCreatedConsumer : IConsumer<DiscountCreatedEvent>
    {
        public async Task Consume(ConsumeContext<DiscountCreatedEvent> context)
        {
            var jsonMessage = JsonSerializer.Serialize(context.Message);
            await Console.Out.WriteLineAsync($"Mensaje from producer : {jsonMessage}");
        }
    }
}
