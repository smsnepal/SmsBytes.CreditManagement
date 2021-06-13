using System.Threading.Tasks;
using SmsBytes.CreditManagement.Storage.Balance;

namespace SmsBytes.CreditManagement.Business.Balance
{
    public interface IBalanceService
    {
        Task<int> GetCredits(string user);
    }

    public class BalanceService : IBalanceService
    {
        private readonly IBalanceRepository _balanceRepository;

        public BalanceService(IBalanceRepository balanceRepository)
        {
            _balanceRepository = balanceRepository;
        }

        public async Task<int> GetCredits(string user)
        {
            return await _balanceRepository.GetSmsBalance(user);
        }
    }
}
