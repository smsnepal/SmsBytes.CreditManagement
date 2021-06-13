using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmsBytes.CreditManagement.Storage.History
{
    public interface ITransactionHistoryRepository
    {
        Task<IEnumerable<Transaction>> GetTopupHistory(string userId);
        Task<IEnumerable<Transaction>> GetTransactionHistory(string userId);
    }

    public class TransactionHistoryRepository : ITransactionHistoryRepository
    {
        private readonly ApplicationContext _db;

        public TransactionHistoryRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Transaction>> GetTopupHistory(string userId)
        {
            return await _db
                .Transactions
                .AsNoTracking()
                .Where(x => x.AccountType == AccountType.Sms)
                .Where(x => x.TransactionType == TransactionType.Debit)
                .Where(x => x.User == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionHistory(string userId)
        {
            return await _db
                .Transactions
                .AsNoTracking()
                .Where(x => x.AccountType == AccountType.Sms)
                .Where(x => x.TransactionType == TransactionType.Credit)
                .Where(x => x.User == userId)
                .ToListAsync();
        }
    }
}
