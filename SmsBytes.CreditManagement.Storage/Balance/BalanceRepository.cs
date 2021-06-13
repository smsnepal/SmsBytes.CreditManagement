using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmsBytes.CreditManagement.Storage.Balance
{
    public interface IBalanceRepository
    {
        public Task<int> GetSmsBalance(string user);
        public Task<int> GetCashBalance(string user);
    }

    public class BalanceRepository : IBalanceRepository
    {
        private readonly ApplicationContext _db;

        public BalanceRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<int> GetSmsBalance(string user)
        {
            return await GetBalance(user, AccountType.Sms);
        }

        public async Task<int> GetCashBalance(string user)
        {
            return await GetBalance(user, AccountType.Cash);
        }

        private async Task<int> GetBalance(string user, AccountType accountType)
        {
            var filtered = _db
                .Transactions.AsNoTracking()
                .Where(x => x.User == user)
                .Where(x => x.AccountType == accountType);
                
            return await filtered
                .Where(x => x.TransactionType == TransactionType.Debit)
                .Select(x => x.Amount)
                .Concat(
                    filtered
                        .Where(x => x.TransactionType == TransactionType.Credit)
                        .Select(x => -x.Amount))
                .SumAsync();
        }
    }
}
