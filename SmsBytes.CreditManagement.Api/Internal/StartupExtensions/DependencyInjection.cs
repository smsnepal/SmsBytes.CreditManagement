using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using SmsBytes.CreditManagement.Business.Balance;
using SmsBytes.CreditManagement.Business.Deduction;
using SmsBytes.CreditManagement.Business.History;
using SmsBytes.CreditManagement.Business.Topup;
using SmsBytes.CreditManagement.Common.Uuid;
using SmsBytes.CreditManagement.Storage;
using SmsBytes.CreditManagement.Storage.Balance;
using SmsBytes.CreditManagement.Storage.History;
using SmsBytes.CreditManagement.Storage.Topup;

namespace SmsBytes.CreditManagement.Api.Internal.StartupExtensions
{
    public static class DependencyInjection
    {
        public static void ConfigureRequiredDependencies(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddDbContext<ApplicationContext>();
            services.AddTransient<DbContext, ApplicationContext>();
            services.AddSingleton<IUuidService, UuidService>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<ITopupService, TopupService>();
            services.AddTransient<IBalanceService, BalanceService>();
            services.AddTransient<IBalanceRepository, BalanceRepository>();
            services.AddTransient<IDeductionService, DeductionService>();
            services.AddTransient<ITransactionHistoryService, TransactionHistoryService>();
            services.AddTransient<ITransactionHistoryRepository, TransactionHistoryRepository>();
        }
    }
}
