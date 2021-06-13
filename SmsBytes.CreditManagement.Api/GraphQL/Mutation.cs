using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using SmsBytes.CreditManagement.Api.GraphQL.Extensions;
using SmsBytes.CreditManagement.Api.GraphQL.Inputs;
using SmsBytes.CreditManagement.Business.Deduction;
using SmsBytes.CreditManagement.Business.Topup;
using SmsBytes.CreditManagement.Storage;
using TransactionType = SmsBytes.CreditManagement.Api.GraphQL.Types.TransactionType;

namespace SmsBytes.CreditManagement.Api.GraphQL
{
    public class Mutation : ObjectGraphType
    {
        public Mutation(ITopupService topupService, IDeductionService deductionService, IHttpContextAccessor contextAccessor)
        {
            FieldAsync<TransactionType, Transaction>("topup",
                arguments: new QueryArguments(TopupInputType.BuildArgument("input")), resolve:
                x => topupService.Topup(x.TopupRequest("input"), contextAccessor.GetUserId()))
                .RequirePermission("credits:topup");

            FieldAsync<TransactionType, Transaction>("deduct",
                arguments: new QueryArguments(DeductionInputType.BuildArgument("input")), resolve:
                x => deductionService.Deduct(x.DeductionRequest("input")))
                .RequirePermission("credits:deduct");
        }
    }
}
