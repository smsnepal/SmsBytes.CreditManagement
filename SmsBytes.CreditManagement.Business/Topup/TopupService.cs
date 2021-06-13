using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmsBytes.CreditManagement.Common.Uuid;
using SmsBytes.CreditManagement.Storage;
using SmsBytes.CreditManagement.Storage.Topup;

namespace SmsBytes.CreditManagement.Business.Topup
{
    public interface ITopupService
    {
        Task<Transaction> Topup(TopupRequest input, string admin);
    }
    public class TopupService : ITopupService
    {
        private readonly IUuidService _uuid;
        private readonly ITransactionRepository _transactionRepository;

        public TopupService(IUuidService uuid, ITransactionRepository transactionRepository)
        {
            _uuid = uuid;
            _transactionRepository = transactionRepository;
        }

        public async Task<Transaction> Topup(TopupRequest input, string admin)
        {
            var txRef = _uuid.GenerateUuId("tx_ref");
            IEnumerable<Transaction> entries = new List<Transaction>(Transaction.TransferCash(input.User, admin, txRef, input.Amount, _uuid));
            entries = entries.Concat(Transaction.TransferCash(admin, "system", txRef, input.Amount, _uuid));
            entries = entries.Concat(Transaction.TransferCash("system", "exchange", txRef, input.Amount, _uuid));
            entries = entries.Concat(Transaction.TransferSms("exchange", "system", txRef, input.Count, _uuid));
            entries = entries.Concat(Transaction.TransferSms("system", input.User, txRef, input.Count, _uuid));
            var results = await _transactionRepository.AddEntries(entries);
            return results.FirstOrDefault(x => x.User == input.User && x.AccountType == AccountType.Sms);
        }
    }
}
