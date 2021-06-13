using Micro.GraphQL.Federation;
using SmsBytes.CreditManagement.Storage;

namespace SmsBytes.CreditManagement.Api.GraphQL.Types
{
    public sealed class TransactionType : ObjectGraphType<Transaction>
    {
        public TransactionType()
        {
            Name = "Transaction";
            Key("id");
            Field("id", x => x.Id);
            Field("amount", x => x.Amount);
            Field("ref", x => x.Ref);
            Field("account_type", x => x.AccountType);
            Field("transaction_type", x => x.TransactionType);
            Field("created_at", x => x.CreatedAt);
        }
    }
}
