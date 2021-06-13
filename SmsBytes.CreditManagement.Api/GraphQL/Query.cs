using System;
using Micro.GraphQL.Federation;
using SmsBytes.CreditManagement.Api.GraphQL.Types;
using SmsBytes.CreditManagement.Storage;

namespace SmsBytes.CreditManagement.Api.GraphQL
{
    public sealed class Query : Query<EntityType>
    {
        public Query()
        {
            Field<Types.TransactionType, Transaction>().Name("transaction").Resolve(x => new Transaction
            {
                Amount = 100,
                Id = "123",
                Ref = "ref",
                User = "asdf",
                CreatedAt = DateTime.Now,
                AccountType = AccountType.Cash,
                TransactionType = Storage.TransactionType.Credit,
            });
        }
    }
}
