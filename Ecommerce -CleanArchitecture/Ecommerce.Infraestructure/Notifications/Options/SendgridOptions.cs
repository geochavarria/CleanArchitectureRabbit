using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infraestructure.Notifications.Options
{
    public class SendgridOptions
    {
        public string ApiKey { get; set; }
        public string FromEmail { get; set; }
        public string FromUser { get; set; }
        public bool SandboxMode { get; set; }
        public string ToAddress { get; set; }
        public string ToUser { get; set; }
    }
}
