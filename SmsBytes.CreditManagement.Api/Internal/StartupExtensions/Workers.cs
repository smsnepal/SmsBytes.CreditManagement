using Microsoft.Extensions.DependencyInjection;
using SmsBytes.CreditManagement.Api.Internal.Workers;

namespace SmsBytes.CreditManagement.Api.Internal.StartupExtensions
{
    public static class Workers
    {
        public static void RegisterWorker(this IServiceCollection services)
        {
            services.AddHostedService<Worker>();
        }
    }
}
