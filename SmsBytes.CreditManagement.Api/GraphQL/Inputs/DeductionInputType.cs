using GraphQL.Types;

namespace SmsBytes.CreditManagement.Api.GraphQL.Inputs
{
    public class DeductionInputType : InputObjectGraphType
    {
        public static QueryArgument BuildArgument(string name)
        {
            return new QueryArgument<NonNullGraphType<DeductionInputType>> {Name = name};
        }

        public DeductionInputType()
        {
            Name = "DeductionInput";
            Field<NonNullGraphType<IntGraphType>>("count");
            Field<NonNullGraphType<StringGraphType>>("user");
            Field<NonNullGraphType<StringGraphType>>("ref");
        }
    }
}
