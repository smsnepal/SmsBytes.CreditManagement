using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SmsBytes.CreditManagement.Business.Topup;
using SmsBytes.CreditManagement.Common.Uuid;
using SmsBytes.CreditManagement.Storage;
using SmsBytes.CreditManagement.Storage.Topup;

namespace SmsBytes.CreditManagement.UnitTest
{
    public class Tests
    {
        [Test]
        public async Task TestTopupCreditsUserCashAndDebitsUserSms()
        {
            var uuidService = new UuidService();
            var mockRepo = new Mock<ITopupRepository>();
            mockRepo.Setup(x => x.Topup(It.IsAny<IEnumerable<Transaction>>())).Returns<IEnumerable<Transaction>>(Task.FromResult);
            var service = new TopupService(uuidService, mockRepo.Object);
            var result = await service.Topup(new TopupRequest
            {
                Amount = 20_000_00,
                Count = 25_000,
                User = "target-user",
            }, "admin-uid");
            Assert.AreEqual(25000, result.Amount);
            Assert.AreEqual(AccountType.Sms, result.AccountType);
            Assert.AreEqual(TransactionType.Debit, result.TransactionType);
            var entries = (IEnumerable<Transaction>) mockRepo.Invocations.First().Arguments[0];
            Assert.AreEqual(1, entries.Count(x => x.AccountType == AccountType.Cash && x.User == "target-user" && x.TransactionType == TransactionType.Credit));
            Assert.AreEqual(20_000_00, entries.First(x => x.AccountType == AccountType.Cash && x.User == "target-user" && x.TransactionType == TransactionType.Credit).Amount);
            Assert.AreEqual(1, entries.Count(x => x.AccountType == AccountType.Sms && x.User == "target-user" && x.TransactionType == TransactionType.Debit));
            Assert.AreEqual(25_000, entries.First(x => x.AccountType == AccountType.Sms && x.User == "target-user" && x.TransactionType == TransactionType.Debit).Amount);
        }

        [Test]
        public async Task TestTopup()
        {
            var uuidService = new UuidService();
            var mockRepo = new Mock<ITopupRepository>();
            mockRepo.Setup(x => x.Topup(It.IsAny<IEnumerable<Transaction>>())).Returns<IEnumerable<Transaction>>(Task.FromResult);
            var service = new TopupService(uuidService, mockRepo.Object);
            var result = await service.Topup(new TopupRequest
            {
                Amount = 20_000_00,
                Count = 25_000,
                User = "target-user",
            }, "admin-uid");
            Assert.AreEqual(25000, result.Amount);
            Assert.AreEqual(AccountType.Sms, result.AccountType);
            Assert.AreEqual(TransactionType.Debit, result.TransactionType);
            var entries = (IEnumerable<Transaction>) mockRepo.Invocations.First().Arguments[0];
            var testHelper = new TestHelper(entries);
            Assert.AreEqual(-20_000_00, testHelper.GetCashValueForUser("target-user"));
            Assert.AreEqual(25_000, testHelper.GetSmsValueForUser("target-user"));
            Assert.AreEqual(0, testHelper.GetSmsValueForUser("admin-uid"));
            Assert.AreEqual(0, testHelper.GetCashValueForUser("admin-uid"));
            Assert.AreEqual(0, testHelper.GetSmsValueForUser("system"));
            Assert.AreEqual(0, testHelper.GetCashValueForUser("system"));
            Assert.AreEqual(20_000_00, testHelper.GetCashValueForUser("exchange"));
            Assert.AreEqual(-25_000, testHelper.GetSmsValueForUser("exchange"));
            Assert.IsTrue(entries.All(x => x.Ref == entries.First().Ref));
        }
    }

    public class TestHelper
    {
        private readonly IEnumerable<Transaction> _entries;

        public TestHelper(IEnumerable<Transaction> entries)
        {
            _entries = entries;
        }

        public int GetSmsValueForUser(string user)
        {
            return _entries
                .Where(x => x.AccountType == AccountType.Sms)
                .Where(x => x.User == user)
                .Select(AbsoluteValue).Sum();
        }

        public int GetCashValueForUser(string user)
        {
            return _entries
                .Where(x => x.AccountType == AccountType.Cash)
                .Where(x => x.User == user)
                .Select(AbsoluteValue).Sum();
        }

        private static int AbsoluteValue(Transaction t)
        {
            if (t.TransactionType == TransactionType.Debit)
            {
                return t.Amount;
            }

            return -t.Amount;
        }
    }
}
