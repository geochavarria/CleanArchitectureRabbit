using commerce.Application.Interface.Infraestructure;
using Ecommerce.Infraestructure.Notifications.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infraestructure.Notifications
{
    internal class NotificationSendgrid : INotification
    {
        private readonly ILogger<NotificationSendgrid> _logger;
        private readonly SendgridOptions _options;
        private readonly ISendGridClient _sendGridClient;

        public NotificationSendgrid(ILogger<NotificationSendgrid> logger, 
            IOptions<SendgridOptions> options,
            ISendGridClient sendGridClient)
        {
            _logger = logger;
            _options = options.Value;
            _sendGridClient = sendGridClient;
        }

        public async Task<bool> SendMailAsync(string subject, string body, CancellationToken cancellation = default)
        {
            SendGridMessage  message =  BuildMessage(subject, body);
            Response? response =  await _sendGridClient
                .SendEmailAsync(message, cancellation)
                .ConfigureAwait(false);


            if(response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Email sent to {_options.ToAddress} at {response.Headers.Date}");
                return true;
            }
            _logger.LogInformation($"Email failed to {_options.ToAddress} with error code {response.StatusCode}");
            return false;
        }


        private SendGridMessage BuildMessage( string subject, string body)
        {
            SendGridMessage message = new SendGridMessage()
            {
                From =  new EmailAddress(_options.FromEmail, _options.FromUser),
                Subject = subject,
            };

            message.AddContent(mimeType: MimeType.Text, body);
            message.AddTo(new EmailAddress(_options.ToAddress, _options.ToUser));

            if (_options.SandboxMode)
            {
                message.MailSettings = new MailSettings() { 
                    SandboxMode = new SandboxMode { 
                        Enable = true 
                    } 
                }; 
           
            }
            return message;
        } 
    }
}
