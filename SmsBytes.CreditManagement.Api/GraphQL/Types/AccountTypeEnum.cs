using GraphQL.Types;
using SmsBytes.CreditManagement.Storage;

namespace SmsBytes.CreditManagement.Api.GraphQL.Types
{
    public class AccountTypeEnum : EnumerationGraphType<AccountType>
    {
        public AccountTypeEnum()
        {
            Name = "AccountType";
        }
    }
}
