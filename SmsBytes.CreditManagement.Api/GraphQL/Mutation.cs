using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using SmsBytes.CreditManagement.Api.GraphQL.Extensions;
using SmsBytes.CreditManagement.Api.GraphQL.Inputs;
using SmsBytes.CreditManagement.Business.Topup;
using SmsBytes.CreditManagement.Storage;
using TransactionType = SmsBytes.CreditManagement.Api.GraphQL.Types.TransactionType;

namespace SmsBytes.CreditManagement.Api.GraphQL
{
    public class Mutation : ObjectGraphType
    {
        public Mutation(ITopupService topupService, IHttpContextAccessor contextAccessor)
        {
            FieldAsync<TransactionType, Transaction>("topup",
                arguments: new QueryArguments(TopupInputType.BuildArgument("input")), resolve:
                x => topupService.Topup(x.TopupRequest("input"), contextAccessor.GetUserId()))
                .RequirePermission("topup.create");
        }
    }
}
