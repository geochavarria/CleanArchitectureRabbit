using Ecommerce.Infraestructure.EventBus.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infraestructure.Notifications.Options
{
    public class SendgridOptionsSetup : IConfigureOptions<SendgridOptions>
    {
        private readonly IConfiguration _configuration;
        private const string ConfigurationSectionName = "Sendgrid";

        public SendgridOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(SendgridOptions options)
        {
            _configuration.GetSection(ConfigurationSectionName).Bind(options);
        }
    }
}
