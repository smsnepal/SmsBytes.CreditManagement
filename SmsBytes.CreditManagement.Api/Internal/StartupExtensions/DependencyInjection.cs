using Microsoft.Extensions.DependencyInjection;
using SmsBytes.CreditManagement.Common.Uuid;
using SmsBytes.CreditManagement.Storage;

namespace SmsBytes.CreditManagement.Api.Internal.StartupExtensions
{
    public static class DependencyInjection
    {
        public static void ConfigureRequiredDependencies(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddDbContext<ApplicationContext>();
            services.AddScoped<IWeatherRepository, WeatherRepository>();
            services.AddSingleton<IUuidService, UuidService>();
        }
    }
}