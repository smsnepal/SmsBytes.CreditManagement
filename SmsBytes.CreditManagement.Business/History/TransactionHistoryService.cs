using System.Collections.Generic;
using System.Threading.Tasks;
using SmsBytes.CreditManagement.Storage;
using SmsBytes.CreditManagement.Storage.History;

namespace SmsBytes.CreditManagement.Business.History
{
    public interface ITransactionHistoryService
    {
        Task<IEnumerable<Transaction>> GetTopupHistory(string userId);
        Task<IEnumerable<Transaction>> GetTransactionHistory(string userId);
    }
    public class TransactionHistoryService : ITransactionHistoryService
    {
        private readonly ITransactionHistoryRepository _transactionHistory;

        public TransactionHistoryService(ITransactionHistoryRepository transactionHistory)
        {
            _transactionHistory = transactionHistory;
        }

        public async Task<IEnumerable<Transaction>> GetTopupHistory(string userId)
        {
            return await _transactionHistory.GetTopupHistory(userId);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionHistory(string userId)
        {
            return await _transactionHistory.GetTransactionHistory(userId);
        }
    }
}
