using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infraestructure.EventBus.Options
{
    public class RabbitMqOptionsSetup : IConfigureOptions<RabbitMqOptions>
    {
        private readonly IConfiguration _configuration;
        private const string ConfigurationSectionName = "RabbitMqOptions";


        public RabbitMqOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(RabbitMqOptions options)
        {
            _configuration.GetSection(ConfigurationSectionName).Bind(options);   
        }
    }
}
