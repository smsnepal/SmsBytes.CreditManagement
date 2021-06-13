using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmsBytes.CreditManagement.Business.Topup;
using SmsBytes.CreditManagement.Common.Uuid;
using SmsBytes.CreditManagement.Storage;
using SmsBytes.CreditManagement.Storage.Topup;

namespace SmsBytes.CreditManagement.Api.Internal.StartupExtensions
{
    public static class DependencyInjection
    {
        public static void ConfigureRequiredDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddDbContext<ApplicationContext>();
            services.AddTransient<DbContext, ApplicationContext>();
            services.AddSingleton<IUuidService, UuidService>();
            services.AddTransient<ITopupRepository, TopupRepository>();
            services.AddTransient<ITopupService, TopupService>();
        }
    }
}