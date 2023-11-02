using commerce.Application.Interface.Infraestructure;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infraestructure.EventBus
{
    public class EventBusRabbitMQ : IEventBus
    {
        private readonly IPublishEndpoint _publishEndPoint;

        public EventBusRabbitMQ(IPublishEndpoint publishEndpoint)
        {
          this._publishEndPoint =  publishEndpoint;
        }
        public async void Publish<T>(T @event)
        {
            await _publishEndPoint.Publish(@event);
        }
    }
}
