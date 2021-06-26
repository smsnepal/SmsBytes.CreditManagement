namespace SmsBytes.CreditManagement.Api.GraphQL.Types
{
    public class EntityType : Micro.GraphQL.Federation.Types.EntityType
    {
        public EntityType()
        {
            Type<TransactionType>();
            Type<UserType>();
        }
    }
}
