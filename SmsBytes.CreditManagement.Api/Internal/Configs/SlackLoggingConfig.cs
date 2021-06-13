using Microsoft.Extensions.Logging;

namespace SmsBytes.CreditManagement.Api.Internal.Configs
{
    public class SlackLoggingConfig
    {
        public string WebhookUrl { set; get; }
        public LogLevel MinLogLevel { set; get; }
    }
}
