using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commerce.Application.Interface.Infraestructure
{
    public interface INotification
    {
        Task<bool> SendMailAsync(string subject, string body, CancellationToken cancellation =  default);
    }
}
