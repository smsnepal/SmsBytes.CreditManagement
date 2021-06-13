using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmsBytes.CreditManagement.Storage.Topup
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> AddEntries(IEnumerable<Transaction> transactions);
    }
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationContext _db;

        public TransactionRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Transaction>> AddEntries(IEnumerable<Transaction> transactions)
        {
            await _db.Transactions.AddRangeAsync(transactions);
            await _db.SaveChangesAsync();
            return transactions;
        }
    }
}
