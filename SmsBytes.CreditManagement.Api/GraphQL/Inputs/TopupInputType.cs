using GraphQL.Types;

namespace SmsBytes.CreditManagement.Api.GraphQL.Inputs
{
    public class TopupInputType : InputObjectGraphType
    {
        public static QueryArgument BuildArgument(string name)
        {
            return new QueryArgument<NonNullGraphType<TopupInputType>> {Name = name};
        }
        public TopupInputType()
        {
            Name = "TopupInput";
            Field<NonNullGraphType<IntGraphType>>("count");
            Field<NonNullGraphType<IntGraphType>>("amount");
            Field<NonNullGraphType<StringGraphType>>("user");
        }
    }
}
