using System.Linq;
using System.Threading.Tasks;
using SmsBytes.CreditManagement.Business.Balance;
using SmsBytes.CreditManagement.Common.Uuid;
using SmsBytes.CreditManagement.Storage;
using SmsBytes.CreditManagement.Storage.Topup;

namespace SmsBytes.CreditManagement.Business.Deduction
{
    public interface IDeductionService
    {
        Task<Transaction> Deduct(DeductionRequest request);
    }
    public class DeductionService : IDeductionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBalanceService _balanceService;
        private readonly IUuidService _uuidService;

        public DeductionService(ITransactionRepository transactionRepository, IUuidService uuidService, IBalanceService balanceService)
        {
            _transactionRepository = transactionRepository;
            _uuidService = uuidService;
            _balanceService = balanceService;
        }

        public async Task<Transaction> Deduct(DeductionRequest request)
        {
            var balance = await _balanceService.GetCredits(request.User);
            if (balance <= 0)
            {
                throw new InsufficientBalanceException();
            }
            var entries = Transaction.TransferSms(request.User, "system", request.Ref, request.Count, _uuidService);
            entries = await _transactionRepository.AddEntries(entries);
            return entries.First(x => x.User == request.User);
        }
    }
}
