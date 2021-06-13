using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmsBytes.CreditManagement.Storage.Topup
{
    public interface ITopupRepository
    {
        Task<IEnumerable<Transaction>> Topup(IEnumerable<Transaction> transactions);
    }
    public class TopupRepository : ITopupRepository
    {
        private readonly ApplicationContext _db;

        public TopupRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Transaction>> Topup(IEnumerable<Transaction> transactions)
        {
            await _db.Transactions.AddRangeAsync(transactions);
            await _db.SaveChangesAsync();
            return transactions;
        }
    }
}
