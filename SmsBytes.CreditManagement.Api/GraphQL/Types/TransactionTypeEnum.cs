using GraphQL.Types;

namespace SmsBytes.CreditManagement.Api.GraphQL.Types
{
    public class TransactionTypeEnum : EnumerationGraphType<Storage.TransactionType>
    {
        public TransactionTypeEnum()
        {
            Name = "TransactionType";
        }
    }
}
