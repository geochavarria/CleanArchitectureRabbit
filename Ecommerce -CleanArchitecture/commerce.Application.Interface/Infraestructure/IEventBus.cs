using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commerce.Application.Interface.Infraestructure
{
    public interface IEventBus
    {
        void Publish<T>(T @event);
    }
}
