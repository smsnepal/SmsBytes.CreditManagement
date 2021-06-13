using GraphQL;
using SmsBytes.CreditManagement.Business.Deduction;
using SmsBytes.CreditManagement.Business.Topup;

namespace SmsBytes.CreditManagement.Api.GraphQL.Extensions
{
    public static class Inputs
    {
        public static TopupRequest TopupRequest(this IResolveFieldContext x, string name)
        {
            return x.GetArgument<TopupRequest>(name);
        }
        public static DeductionRequest DeductionRequest(this IResolveFieldContext x, string name)
        {
            return x.GetArgument<DeductionRequest>(name);
        }
    }
}
