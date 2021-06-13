using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Slack;
using SmsBytes.CreditManagement.Api.Internal.Configs;

namespace SmsBytes.CreditManagement.Api.Internal.StartupExtensions
{
    public static class Logger
    {
        public static void ConfigureLoggerWithSlack(this ILoggerFactory loggerFactory, SlackLoggingConfig slackConfig,
            IHostEnvironment env)
        {
            if (string.IsNullOrEmpty(slackConfig.WebhookUrl))
            {
                return;
            }
            loggerFactory.AddSlack(new SlackConfiguration
            {
                MinLevel = slackConfig.MinLogLevel,
                WebhookUrl = new Uri(slackConfig.WebhookUrl)
            }, env.ApplicationName, env.EnvironmentName);
        }
    }
}