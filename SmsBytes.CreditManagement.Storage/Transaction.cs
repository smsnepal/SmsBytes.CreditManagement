using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SmsBytes.CreditManagement.Common.Uuid;

namespace SmsBytes.CreditManagement.Storage
{
    public class Transaction
    {
        public string Id { set; get; }

        [StringLength(50)]
        public string User { set; get; }

        public int Amount { set; get; }
        public TransactionType TransactionType { set; get; }
        public AccountType AccountType { set; get; }

        [StringLength(80)]
        public string Ref { set; get; }

        public DateTime CreatedAt { set; get; }

        public static IEnumerable<Transaction> TransferCash(string from, string to, string txRef, int amount, IUuidService uuidService)
        {
            return new List<Transaction>
            {
                new Transaction
                {
                    Id = uuidService.GenerateUuId("transaction"),
                    Amount = amount,
                    AccountType = AccountType.Cash,
                    CreatedAt = DateTime.Now,
                    Ref = txRef,
                    TransactionType = TransactionType.Credit,
                    User = from,
                },
                new Transaction
                {
                    Id = uuidService.GenerateUuId("transaction"),
                    Amount = amount,
                    AccountType = AccountType.Cash,
                    CreatedAt = DateTime.Now,
                    Ref = txRef,
                    TransactionType = TransactionType.Debit,
                    User = to,
                },
            };
        }
        public static IEnumerable<Transaction> TransferSms(string from, string to, string txRef, int amount, IUuidService uuidService)
        {
            return new List<Transaction>
            {
                new Transaction
                {
                    Id = uuidService.GenerateUuId("transaction"),
                    Amount = amount,
                    AccountType = AccountType.Sms,
                    CreatedAt = DateTime.Now,
                    Ref = txRef,
                    TransactionType = TransactionType.Credit,
                    User = from,
                },
                new Transaction
                {
                    Id = uuidService.GenerateUuId("transaction"),
                    Amount = amount,
                    AccountType = AccountType.Sms,
                    CreatedAt = DateTime.Now,
                    Ref = txRef,
                    TransactionType = TransactionType.Debit,
                    User = to,
                },
            };
        }
    }

    public enum AccountType
    {
        Cash = 1,
        Sms = 2,
    }

    public enum TransactionType
    {
        Debit = 1,
        Credit = 2,
    }
}
