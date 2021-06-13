using System.Collections.Generic;
using GraphQL;
using GraphQL.Types;
using Micro.GraphQL.Federation;
using Microsoft.AspNetCore.Http;
using SmsBytes.CreditManagement.Api.GraphQL.Extensions;
using SmsBytes.CreditManagement.Api.GraphQL.Types;
using SmsBytes.CreditManagement.Business.Balance;
using SmsBytes.CreditManagement.Business.History;
using SmsBytes.CreditManagement.Storage;
using TransactionType = SmsBytes.CreditManagement.Api.GraphQL.Types.TransactionType;

namespace SmsBytes.CreditManagement.Api.GraphQL
{
    public sealed class Query : Query<EntityType>
    {
        public Query(ITransactionHistoryService historyService, IBalanceService balanceService,
            IHttpContextAccessor httpContextAccessor)
        {
            Field<NonNullGraphType<ListGraphType<TransactionType>>, IEnumerable<Transaction>>()
                .Name("transaction_history")
                .ResolveAsync(async x => await historyService.GetTransactionHistory(httpContextAccessor.GetUserId()))
                .Authorize();

            FieldAsync<NonNullGraphType<IntGraphType>, int>("credits",
                    arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "user"}),
                    resolve: async x => await balanceService.GetCredits(x.GetArgument<string>("user")))
                .RequirePermission("admin:credits:view");
        }
    }
}
